using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

// =========================
// INTERFACES
// =========================
public interface ITributo
{
    double CalcularValorIPVA();
}

// =========================
// ENTIDADES / MODELS
// =========================
public class Endereco
{
    public string rua { get; set; }
    public string numero { get; set; }
    public string bairro { get; set; }
    public string cidade { get; set; }
    public string estado { get; set; }
    public string CEP { get; set; }

    public Endereco() { }

    public Endereco(string rua, string numero, string bairro, string cidade, string estado, string cep)
    {
        if (string.IsNullOrWhiteSpace(cep))
            throw new ArgumentException("CEP inválido.", nameof(cep));

        this.rua = rua;
        this.numero = numero;
        this.bairro = bairro;
        this.cidade = cidade;
        this.estado = estado;
        this.CEP = cep;
    }

    public override string ToString()
    {
        return $"{rua}, {numero} - {bairro}, {cidade} - {estado}, CEP: {CEP}";
    }
}

public abstract class Pessoa
{
    public string nome { get; set; }
    public string CPF { get; set; }
    public string telefone { get; set; }
    public string email { get; set; }
    public Endereco endereco { get; set; }

    public Pessoa() { }

    protected Pessoa(string nome, string cpf, string telefone, string email, Endereco endereco)
    {
        if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11)
            throw new ArgumentException("CPF deve conter 11 dígitos.", nameof(cpf));

        this.nome = nome;
        this.CPF = cpf;
        this.telefone = telefone;
        this.email = email;
        this.endereco = endereco;
    }

    public virtual void ExibirInfo()
    {
        Console.WriteLine($"Nome: {nome}");
        Console.WriteLine($"CPF: {CPF}");
        Console.WriteLine($"Telefone: {telefone}");
        Console.WriteLine($"E-mail: {email}");
        Console.WriteLine($"Endereço: {endereco}");
    }
}

public class Motorista : Pessoa
{
    public int idMotorista { get; set; }
    public string numeroCNH { get; set; }
    public string categoriaCNH { get; set; }
    public string status { get; set; }

    public Motorista() { }

    public Motorista(int idMotorista, string numeroCNH, string categoriaCNH, string status,
        string nome, string cpf, string telefone, string email, Endereco endereco)
        : base(nome, cpf, telefone, email, endereco)
    {
        this.idMotorista = idMotorista;
        this.numeroCNH = numeroCNH;
        this.categoriaCNH = categoriaCNH;
        this.status = status;
    }

    public override void ExibirInfo()
    {
        Console.WriteLine("=== MOTORISTA ===");
        Console.WriteLine($"ID Motorista: {idMotorista}");
        Console.WriteLine($"CNH: {numeroCNH}");
        Console.WriteLine($"Categoria: {categoriaCNH}");
        Console.WriteLine($"Status: {status}");
        base.ExibirInfo();
    }
}

public class Cliente : Pessoa
{
    public int idCliente { get; set; }
    public string tipoCliente { get; set; }

    public Cliente() { }

    public Cliente(int idCliente, string tipoCliente, string nome, string cpf, string telefone, string email, Endereco endereco)
        : base(nome, cpf, telefone, email, endereco)
    {
        this.idCliente = idCliente;
        this.tipoCliente = tipoCliente;
    }

    public override void ExibirInfo()
    {
        Console.WriteLine("=== CLIENTE ===");
        Console.WriteLine($"ID Cliente: {idCliente}");
        Console.WriteLine($"Tipo de Cliente: {tipoCliente}");
        base.ExibirInfo();
    }
}

// =========================
// VEÍCULOS + CONVERSOR POLIMÓRFICO
// =========================
[JsonConverter(typeof(VeiculoJsonConverter))]
public abstract class Veiculo
{
    public string modelo { get; set; }
    public int ano { get; set; }
    public string cor { get; set; }
    public string marca { get; set; }
    public string placa { get; set; }
    public double valor { get; set; }
    public int qtdRodas { get; set; }

    public Veiculo() { }

    protected Veiculo(string modelo, int ano, string cor, string marca, string placa, double valor, int qtdRodas)
    {
        this.modelo = modelo;
        this.ano = ano;
        this.cor = cor;
        this.marca = marca;
        this.placa = placa;
        this.valor = valor;
        this.qtdRodas = qtdRodas;
    }

    public abstract void ExibirInfo();
    public abstract bool PodeTransportar(double peso);
}

public class Carro : Veiculo, ITributo
{
    public int numeroPortas { get; set; }

    public Carro() { }

    public Carro(string modelo, int ano, string cor, string marca, string placa, double valor, int numeroPortas)
        : base(modelo, ano, cor, marca, placa, valor, 4)
    {
        this.numeroPortas = numeroPortas;
    }

    public override void ExibirInfo()
    {
        Console.WriteLine("=== CARRO ===");
        Console.WriteLine($"Modelo: {modelo}");
        Console.WriteLine($"Ano: {ano}");
        Console.WriteLine($"Cor: {cor}");
        Console.WriteLine($"Marca: {marca}");
        Console.WriteLine($"Placa: {placa}");
        Console.WriteLine($"Portas: {numeroPortas}");
    }

    public override bool PodeTransportar(double peso) => peso <= 500;

    public double CalcularValorIPVA() => valor * 0.04;
}

public class Moto : Veiculo, ITributo
{
    public int cilindrada { get; set; }

    public Moto() { }

    public Moto(string modelo, int ano, string cor, string marca, string placa, double valor, int cilindrada)
        : base(modelo, ano, cor, marca, placa, valor, 2)
    {
        this.cilindrada = cilindrada;
    }

    public override void ExibirInfo()
    {
        Console.WriteLine("=== MOTO ===");
        Console.WriteLine($"Modelo: {modelo}");
        Console.WriteLine($"Ano: {ano}");
        Console.WriteLine($"Cilindrada: {cilindrada}");
    }

    public override bool PodeTransportar(double peso) => peso <= 150;

    public double CalcularValorIPVA() => valor * 0.02;
}

public class Caminhao : Veiculo, ITributo
{
    public double capacidadeCargaKg { get; set; }
    public int numeroEixo { get; set; }

    public Caminhao() { }

    public Caminhao(string modelo, int ano, string cor, string marca, string placa, double valor, double capacidade, int eixos)
        : base(modelo, ano, cor, marca, placa, valor, eixos * 2)
    {
        capacidadeCargaKg = capacidade;
        numeroEixo = eixos;
    }

    public override void ExibirInfo()
    {
        Console.WriteLine("=== CAMINHÃO ===");
        Console.WriteLine($"Modelo: {modelo}");
        Console.WriteLine($"Ano: {ano}");
        Console.WriteLine($"Capacidade: {capacidadeCargaKg}kg");
    }

    public override bool PodeTransportar(double peso) => peso <= capacidadeCargaKg;

    public double CalcularValorIPVA() => valor * 0.03;
}

// Conversor JSON para Veiculo (escreve/ler um campo "tipoVeiculo" para polimorfismo)
public class VeiculoJsonConverter : JsonConverter<Veiculo>
{
    public override Veiculo Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (var doc = JsonDocument.ParseValue(ref reader))
        {
            var root = doc.RootElement;

            if (!root.TryGetProperty("tipoVeiculo", out JsonElement tipoElem))
            {
                throw new JsonException("Propriedade 'tipoVeiculo' não encontrada ao desserializar Veiculo.");
            }

            string tipo = tipoElem.GetString()?.ToLowerInvariant();
            Type destino = tipo switch
            {
                "carro" => typeof(Carro),
                "moto" => typeof(Moto),
                "caminhao" => typeof(Caminhao),
                _ => throw new JsonException($"Tipo de veículo desconhecido: {tipo}")
            };

            var raw = root.GetRawText();
            var veiculo = (Veiculo)JsonSerializer.Deserialize(raw, destino, options);
            return veiculo;
        }
    }

    public override void Write(Utf8JsonWriter writer, Veiculo value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        string tipo = value switch
        {
            Carro => "carro",
            Moto => "moto",
            Caminhao => "caminhao",
            _ => "veiculo"
        };

        writer.WriteString("tipoVeiculo", tipo);

        // Serializa o objeto real (usando o tipo concreto) e copia as propriedades
        var raw = JsonSerializer.Serialize(value, value.GetType(), new JsonSerializerOptions
        {
            PropertyNamingPolicy = options.PropertyNamingPolicy,
            WriteIndented = false
        });

        using (var doc = JsonDocument.Parse(raw))
        {
            foreach (var prop in doc.RootElement.EnumerateObject())
            {
                prop.WriteTo(writer);
            }
        }

        writer.WriteEndObject();
    }
}

// =========================
// ENTREGA
// =========================
public class Entrega
{
    public int idEntrega { get; set; }
    public Cliente cliente { get; set; }
    public Motorista motorista { get; set; }
    public Veiculo veiculo { get; set; }
    public Endereco enderecoOrigem { get; set; }
    public Endereco enderecoDestino { get; set; }
    public DateTime dataEntrega { get; set; }
    public string status { get; set; }
    public double distanciaKm { get; set; }

    public Entrega() { }

    public Entrega(int idEntrega, Cliente cliente, Motorista motorista, Veiculo veiculo,
        Endereco origem, Endereco destino, DateTime data, double distanciaKm, string status = "Pendente")
    {
        if (distanciaKm <= 0)
            throw new ArgumentException("Distância deve ser maior que zero.", nameof(distanciaKm));

        this.idEntrega = idEntrega;
        this.cliente = cliente;
        this.motorista = motorista;
        this.veiculo = veiculo;
        this.enderecoOrigem = origem;
        this.enderecoDestino = destino;
        this.dataEntrega = data;
        this.distanciaKm = distanciaKm;
        this.status = status;
    }

    public void ExibirResumo()
    {
        Console.WriteLine($"Entrega {idEntrega}: {cliente.nome} -> {enderecoDestino}");
    }

    public void AtualizarStatus(string novoStatus)
    {
        status = novoStatus;
    }

    public void CalcularTempo()
    {
        Console.WriteLine($"Tempo estimado: {distanciaKm / 60:F2}h");
    }

    public void CalcularCusto()
    {
        Console.WriteLine($"Custo estimado: R$ {distanciaKm * 3.5:F2}");
    }
}

// =========================
// FACTORY
// =========================
public static class VeiculoFactory
{
    public static Veiculo CriarVeiculo(string tipo)
    {
        tipo = (tipo ?? "").ToLowerInvariant();

        return tipo switch
        {
            "carro" => new Carro("Uno", 2012, "Branco", "Fiat", "AAA-1111", 20000, 4),
            "moto" => new Moto("CG", 2018, "Vermelha", "Honda", "BBB-2222", 9000, 150),
            "caminhao" => new Caminhao("Mercedes 1113", 2005, "Azul", "Mercedes", "CCC-3333", 120000, 8000, 2),
            _ => throw new ArgumentException("Tipo de veículo inválido.", nameof(tipo))
        };
    }
}

// =========================
// REPOSITÓRIO
// =========================
public class RepositorioEntregas
{
    private readonly string arquivo;
    private List<Entrega> entregas = new List<Entrega>();

    private JsonSerializerOptions JsonOptions => new JsonSerializerOptions()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new VeiculoJsonConverter() } // garante polimorfismo para Veiculo
    };

    public RepositorioEntregas(string arquivo)
    {
        this.arquivo = arquivo;
    }

    public void Adicionar(Entrega entrega)
    {
        entregas.Add(entrega);
    }

    public List<Entrega> ObterTodos() => entregas;

    public int GerarId() => entregas.Count + 1;

    public void Salvar()
    {
        try
        {
            var json = JsonSerializer.Serialize(entregas, JsonOptions);
            File.WriteAllText(arquivo, json);
            Console.WriteLine($"Dados salvos em {arquivo}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao salvar: {ex.Message}");
        }
    }

    public void Carregar()
    {
        try
        {
            if (!File.Exists(arquivo))
            {
                Console.WriteLine("Nenhum arquivo encontrado.");
                return;
            }

            var json = File.ReadAllText(arquivo);
            entregas = JsonSerializer.Deserialize<List<Entrega>>(json, JsonOptions) ?? new List<Entrega>();
            Console.WriteLine("Dados carregados com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao carregar: {ex.Message}");
        }
    }
}

// =========================
// PROGRAMA (Menu)
// =========================
public class Program
{
    static RepositorioEntregas repo = new RepositorioEntregas("entregas.json");

    public static void Main(string[] args)
    {
        Console.WriteLine("=== SISTEMA DE LOGÍSTICA ===");

        int opcao = 0;

        do
        {
            Console.WriteLine("\nMenu Principal:");
            Console.WriteLine("1 - Registrar nova entrega");
            Console.WriteLine("2 - Listar entregas");
            Console.WriteLine("3 - Salvar entregas em arquivo");
            Console.WriteLine("4 - Carregar entregas do arquivo");
            Console.WriteLine("5 - Exibir detalhes de uma entrega");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");

            try
            {
                opcao = int.Parse(Console.ReadLine() ?? "0");
            }
            catch
            {
                Console.WriteLine("Entrada inválida. Digite apenas números.");
                continue;
            }

            switch (opcao)
            {
                case 1:
                    RegistrarEntrega();
                    break;
                case 2:
                    ListarEntregas();
                    break;
                case 3:
                    repo.Salvar();
                    break;
                case 4:
                    repo.Carregar();
                    break;
                case 5:
                    ExibirDetalhes();
                    break;
                case 0:
                    Console.WriteLine("Encerrando...");
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

        } while (opcao != 0);
    }

    static void RegistrarEntrega()
    {
        try
        {
            Console.WriteLine("\n--- Cadastro de Entrega ---");

            Console.Write("Nome do cliente: ");
            string nomeCliente = Console.ReadLine() ?? "";

            Console.Write("CPF (11 dígitos): ");
            string cpf = Console.ReadLine() ?? "";

            if (cpf.Length != 11 || !cpf.All(char.IsDigit))
                throw new ArgumentException("CPF deve conter 11 dígitos numéricos.");

            // Endereço simplificado (em produção, pedir ao usuário)
            Endereco end = new Endereco("Rua X", "10", "Centro", "São Paulo", "SP", "00000-000");

            Cliente cliente = new Cliente(repo.GerarId(), "PF", nomeCliente, cpf, "11999999999", "email@teste.com", end);

            Motorista mot = new Motorista(1, "123456", "B", "Disponível", "Carlos", "11111111111", "11998887777", "c@x.com", end);

            Console.Write("Tipo de veículo (carro/moto/caminhao): ");
            string tipo = (Console.ReadLine() ?? "carro").ToLowerInvariant();

            Veiculo veiculo = VeiculoFactory.CriarVeiculo(tipo);

            Console.Write("Distância em km: ");
            if (!double.TryParse(Console.ReadLine(), out double distancia) || distancia <= 0)
            {
                Console.WriteLine("Distância inválida. Usando 1 km como padrão.");
                distancia = 1;
            }

            Entrega entrega = new Entrega(
    idEntrega: repo.GerarId(),
    cliente: cliente,
    motorista: mot,
    veiculo: veiculo,
    origem: end,
    destino: end,
    data: DateTime.Now,
    distanciaKm: distancia
);


            repo.Adicionar(entrega);

            Console.WriteLine("Entrega registrada com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao cadastrar entrega: {ex.Message}");
        }
    }

    static void ListarEntregas()
    {
        var lista = repo.ObterTodos();

        if (lista.Count == 0)
        {
            Console.WriteLine("Nenhuma entrega registrada.");
            return;
        }

        Console.WriteLine("\n=== ENTREGAS REGISTRADAS ===");

        foreach (var e in lista)
        {
            Console.WriteLine($"ID: {e.idEntrega} - Cliente: {e.cliente?.nome ?? "N/A"} - Veículo: {e.veiculo?.marca} {e.veiculo?.modelo} - Status: {e.status}");
        }
    }

    static void ExibirDetalhes()
    {
        Console.Write("Digite o ID da entrega: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        var e = repo.ObterTodos().Find(x => x.idEntrega == id);
        if (e == null)
        {
            Console.WriteLine("Entrega não encontrada.");
            return;
        }

        Console.WriteLine("\n--- Detalhes da Entrega ---");
        e.ExibirResumo();
        Console.WriteLine($"Data: {e.dataEntrega:dd/MM/yyyy HH:mm}");
        Console.WriteLine($"Distância: {e.distanciaKm} km");
        Console.WriteLine($"Status: {e.status}");
        Console.WriteLine("Cliente:");
        e.cliente?.ExibirInfo();
        Console.WriteLine("Motorista:");
        e.motorista?.ExibirInfo();
        Console.WriteLine("Veículo:");
        e.veiculo?.ExibirInfo();
    }
}
