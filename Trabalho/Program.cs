using System;

// Interface - ITributo
public interface ITributo
{
    double CalcularValorIPVA();
}

// Classe abstrata - Veiculo
public abstract class Veiculo
{
    public string modelo { get; private set; }
    public int ano { get; private set; }
    public string cor { get; private set; }
    public string marca { get; private set; }
    public string placa { get; private set; }
    public double valor { get; private set; }
    public int qtdRodas { get; private set; }

    public Veiculo(string modelo, int ano, string cor, string marca, string placa, double valor, int qtdRodas)
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

// Classe concreta - Carro
public class Carro : Veiculo, ITributo
{
    public int numeroPortas { get; private set; }

    public Carro(string modelo, int ano, string cor, string marca, string placa, double valor, int numeroPortas) : base(modelo, ano, cor, marca, placa, valor, 4)
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

// Classe concreta - Caminhao
public class Caminhao : Veiculo, ITributo
{
    public double capacidadeCargaKg { get; private set; }
    public int numeroEixo { get; private set; }

    public Caminhao(string modelo, int ano, string cor, string marca, string placa, double valor, double capacidadeCargaKg, int numeroEixo) : base(modelo, ano, cor, marca, placa, valor, numeroEixo * 2)
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

// Classe concreta - Moto
public class Moto : Veiculo, ITributo
{
    public int cilindrada { get; private set; }

    public Moto(string modelo, int ano, string cor, string marca, string placa, double valor, int cilindrada) : base(modelo, ano, cor, marca, placa, valor, 2)
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