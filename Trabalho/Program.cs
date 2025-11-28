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