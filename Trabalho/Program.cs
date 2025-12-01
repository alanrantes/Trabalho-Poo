using System;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== SISTEMA DE LOGÍSTICA ===");

        // Criar endereços
        Endereco end1 = new Endereco("Rua A", "100", "Centro", "São Paulo", "SP", "01000-000");
        Endereco end2 = new Endereco("Rua B", "200", "Jardins", "São Paulo", "SP", "02000-000");

        // Criar cliente
        Cliente cliente = new Cliente(
            idCliente: 1,
            tipoCliente: "PF",
            nome: "João Silva",
            cpf: "12345678901",
            telefone: "1199999-9999",
            email: "joao@email.com",
            endereco: end1
        );

        // Criar motorista
        Motorista motorista = new Motorista(
            idMotorista: 10,
            numeroCNH: "44556677",
            categoriaCNH: "B",
            status: "Disponível",
            nome: "Carlos Mendes",
            cpf: "98765432100",
            telefone: "1188888-8888",
            email: "carlos@email.com",
            endereco: end2
        );

        // Criar veículo (exemplo: Carro)
        Veiculo veiculo = new Carro(
            modelo: "Fiesta",
            ano: 2015,
            cor: "Preto",
            marca: "Ford",
            placa: "ABC-1234",
            valor: 35000,
            numeroPortas: 4
        );

        // Criar entrega
        Entrega entrega = new Entrega(
            idEntrega: 1,
            cliente: cliente,
            motorista: motorista,
            veiculo: veiculo,
            enderecoOrigem: end1,
            enderecoDestino: end2,
            dataEntrega: DateTime.Now,
            distanciaKm: 25.5
        );

        // Exibir resumo da entrega
        entrega.ExibirResumo();

        Console.WriteLine("\nCálculo de tempo e custo:");
        entrega.CalcularTempo();
        entrega.CalcularCusto();

        Console.WriteLine("\nPrograma finalizado.");
    }
}

// =========================
// Interface - ITributo
// =========================
public interface ITributo
{
    double CalcularValorIPVA();
}

// =========================
// Classe abstrata - Veiculo
// =========================
public abstract class Veiculo
{
    public string modelo { get; private set; }
    public int ano { get; private set; }
    public string cor { get; private set; }
    public string marca { get; private set; }
    public string placa { get; private set; }
    public double valor { get; private set; }
    public int qtdRodas { get; private set; }

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

// =========================
// Classe concreta - Carro
// =========================
public class Carro : Veiculo, ITributo
{
    public int numeroPortas { get; private set; }

    public Carro(
        string modelo,
        int ano,
        string cor,
        string marca,
        string placa,
        double valor,
        int numeroPortas
    ) : base(modelo, ano, cor, marca, placa, valor, 4)
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
        Console.WriteLine($"Valor: {valor}");
        Console.WriteLine($"Rodas: {qtdRodas}");
        Console.WriteLine($"Portas: {numeroPortas}");
    }

    // Sobrescrita
    public override bool PodeTransportar(double peso)
    {
        return peso <= 500;
    }

    public double CalcularValorIPVA()
    {
        return this.valor * 0.04;
    }
}

// =========================
// Classe concreta - Caminhao
// =========================
public class Caminhao : Veiculo, ITributo
{
    public double capacidadeCargaKg { get; private set; }
    public int numeroEixo { get; private set; }

    public Caminhao(
        string modelo,
        int ano,
        string cor,
        string marca,
        string placa,
        double valor,
        double capacidadeCargaKg,
        int numeroEixo
    ) : base(modelo, ano, cor, marca, placa, valor, numeroEixo * 2)
    {
        this.capacidadeCargaKg = capacidadeCargaKg;
        this.numeroEixo = numeroEixo;
    }

    public override void ExibirInfo()
    {
        Console.WriteLine("=== CAMINHÃO ===");
        Console.WriteLine($"Modelo: {modelo}");
        Console.WriteLine($"Ano: {ano}");
        Console.WriteLine($"Cor: {cor}");
        Console.WriteLine($"Marca: {marca}");
        Console.WriteLine($"Placa: {placa}");
        Console.WriteLine($"Valor: {valor}");
        Console.WriteLine($"Rodas: {qtdRodas}");
        Console.WriteLine($"Capacidade de carga: {capacidadeCargaKg} kg");
        Console.WriteLine($"Número de eixos: {numeroEixo}");
    }

    public override bool PodeTransportar(double peso)
    {
        return peso <= capacidadeCargaKg;
    }

    public double CalcularValorIPVA()
    {
        return this.valor * 0.03;
    }
}

// =========================
// Classe concreta - Moto
// =========================
public class Moto : Veiculo, ITributo
{
    public int cilindrada { get; private set; }

    public Moto(
        string modelo,
        int ano,
        string cor,
        string marca,
        string placa,
        double valor,
        int cilindrada
    ) : base(modelo, ano, cor, marca, placa, valor, 2)
    {
        this.cilindrada = cilindrada;
    }

    public override void ExibirInfo()
    {
        Console.WriteLine("=== MOTO ===");
        Console.WriteLine($"Modelo: {modelo}");
        Console.WriteLine($"Ano: {ano}");
        Console.WriteLine($"Cor: {cor}");
        Console.WriteLine($"Marca: {marca}");
        Console.WriteLine($"Placa: {placa}");
        Console.WriteLine($"Valor: {valor}");
        Console.WriteLine($"Rodas: {qtdRodas}");
        Console.WriteLine($"Cilindrada: {cilindrada} cc");
    }

    public override bool PodeTransportar(double peso)
    {
        return peso <= 150;
    }

    public double CalcularValorIPVA()
    {
        return this.valor * 0.02;
    }
}

// =========================
// Classe - Endereco
// =========================
public class Endereco
{
    public string rua { get; private set; }
    public string numero { get; private set; }
    public string bairro { get; private set; }
    public string cidade { get; private set; }
    public string estado { get; private set; }
    public string CEP { get; private set; }

    public Endereco(
        string rua,
        string numero,
        string bairro,
        string cidade,
        string estado,
        string cep
    )
    {
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

// =========================
// Classe abstrata - Pessoa
// (base para Cliente e Motorista, conforme diagrama)
// =========================
public abstract class Pessoa
{
    public string nome { get; protected set; }
    public string CPF { get; protected set; }
    public string telefone { get; protected set; }
    public string email { get; protected set; }
    public Endereco endereco { get; protected set; }

    protected Pessoa(
        string nome,
        string cpf,
        string telefone,
        string email,
        Endereco endereco
    )
    {
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

    public bool ValidarCPF()
    {
        // Validação simples apenas para exemplo
        if (string.IsNullOrWhiteSpace(CPF))
            return false;

        string apenasNumeros = "";
        foreach (char c in CPF)
        {
            if (char.IsDigit(c))
                apenasNumeros += c;
        }

        // CPF brasileiro tem 11 dígitos
        return apenasNumeros.Length == 11;
    }
}

// =========================
// Classe - Cliente
// (herda de Pessoa)
// =========================
public class Cliente : Pessoa
{
    public int idCliente { get; private set; }
    public string tipoCliente { get; private set; } // Ex.: "PJ", "PF", "VIP"

    public Cliente(
        int idCliente,
        string tipoCliente,
        string nome,
        string cpf,
        string telefone,
        string email,
        Endereco endereco
    ) : base(nome, cpf, telefone, email, endereco)
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

    // Poderia ler novos dados do usuário ou vir de outra parte do sistema;
    // aqui apenas foi deixada uma lógica simples para exemplo.
    public void AtualizarCadastro()
    {
        Console.WriteLine("Cadastro do cliente atualizado (simulação).");
    }
}

// =========================
// Classe - Motorista
// (herda de Pessoa, usado em Entrega)
// =========================
public class Motorista : Pessoa
{
    public int idMotorista { get; private set; }
    public string numeroCNH { get; private set; }
    public string categoriaCNH { get; private set; }
    public string status { get; private set; }

    public Motorista(
        int idMotorista,
        string numeroCNH,
        string categoriaCNH,
        string status,
        string nome,
        string cpf,
        string telefone,
        string email,
        Endereco endereco
    ) : base(nome, cpf, telefone, email, endereco)
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

    public void AtualizarStatus(string novoStatus)
    {
        status = novoStatus;
        Console.WriteLine($"Status do motorista atualizado para: {status}");
    }
}

// =========================
// Classe - Entrega
// =========================
public class Entrega
{
    public int idEntrega { get; private set; }
    public Cliente cliente { get; private set; }
    public Motorista motorista { get; private set; }
    public Veiculo veiculo { get; private set; }
    public Endereco enderecoOrigem { get; private set; }
    public Endereco enderecoDestino { get; private set; }
    public DateTime dataEntrega { get; private set; }
    public string status { get; private set; }
    public double distanciaKm { get; private set; }

    public Entrega(
        int idEntrega,
        Cliente cliente,
        Motorista motorista,
        Veiculo veiculo,
        Endereco enderecoOrigem,
        Endereco enderecoDestino,
        DateTime dataEntrega,
        double distanciaKm,
        string status = "Pendente"
    )
    {
        this.idEntrega = idEntrega;
        this.cliente = cliente;
        this.motorista = motorista;
        this.veiculo = veiculo;
        this.enderecoOrigem = enderecoOrigem;
        this.enderecoDestino = enderecoDestino;
        this.dataEntrega = dataEntrega;
        this.distanciaKm = distanciaKm;
        this.status = status;
    }

    public void ExibirResumo()
    {
        Console.WriteLine("=== RESUMO DA ENTREGA ===");
        Console.WriteLine($"ID Entrega: {idEntrega}");
        Console.WriteLine($"Data: {dataEntrega:dd/MM/yyyy}");
        Console.WriteLine($"Status: {status}");
        Console.WriteLine($"Distância: {distanciaKm} km");
        Console.WriteLine($"Origem: {enderecoOrigem}");
        Console.WriteLine($"Destino: {enderecoDestino}");
        Console.WriteLine($"Cliente: {cliente.nome}");
        Console.WriteLine($"Motorista: {motorista.nome}");
        Console.WriteLine($"Veículo: {veiculo.marca} {veiculo.modelo} - Placa {veiculo.placa}");
    }

    public void AtualizarStatus(string novoStatus)
    {
        status = novoStatus;
        Console.WriteLine($"Status da entrega atualizado para: {status}");
    }

    public void CalcularTempo()
    {
        const double velocidadeMediaKmH = 60.0;

        if (distanciaKm <= 0)
        {
            Console.WriteLine("Distância inválida para cálculo de tempo.");
            return;
        }

        double tempoHoras = distanciaKm / velocidadeMediaKmH;
        Console.WriteLine($"Tempo estimado de entrega: {tempoHoras:F2} horas.");
    }

    public void CalcularCusto()
    {
        // Exemplo simples: custo fixo por km
        const double custoPorKm = 3.5;
        double custo = distanciaKm * custoPorKm;

        Console.WriteLine($"Custo estimado da entrega: R$ {custo:F2}");
    }
}