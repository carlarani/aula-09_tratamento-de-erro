/* Main */

var resultado = 0.0;
var input = 0;

do
{
    input = GetMainInput(resultado);

    try
    {
        if (!IsMathematicalOperation(input))
        {
            DoNonMathematicalOperation(input, ref resultado);
            continue;
        }

        var newValue = GetMathematicalInput(input);
        resultado = Calculate(input, newValue, resultado);
    }
    catch (DivideByZeroException ex)
    {
        Console.WriteLine(
            ex.Message + Environment.NewLine + 
            $"Tipo do Erro: {ex.GetType()}" + Environment.NewLine +
            "Localização do Erro: " + Environment.NewLine + ex.StackTrace + Environment.NewLine + Environment.NewLine +
            "Aperte Enter para continuar..."
        );

        Console.ReadLine();
    }
    finally
    {
        Console.Clear();

        // Aparentemente o terminal do Windows é lento demais para
        // apagar a tela, então precisamos fazer o programa esperar
        // um pouco para as intruções não saírem cortadas
        Thread.Sleep(100);
    }

} while (input is > 0 and < 8);



/* Funções */

static int GetMainInput(double valorAtual)
{
    Console.WriteLine(
        "Escolha qual operação deseja realizar: " + Environment.NewLine +
        "1 - Somar" + Environment.NewLine +
        "2 - Subtrair" + Environment.NewLine +
        "3 - Multiplicar" + Environment.NewLine +
        "4 - Dividir" + Environment.NewLine +
        "5 - Zerar calculadora" + Environment.NewLine +
        "6 - Retornar último valor da operação" + Environment.NewLine +
        "7 - Definir valor da calculadora" + Environment.NewLine +
        "Outro Número - Sair" + Environment.NewLine + Environment.NewLine +
        $"Valor Atual: {valorAtual}"
    );

    return GetIntInput();
}

static void DoNonMathematicalOperation(int operationType, ref double calcValue)
{
    var message = operationType switch
    {
        5 => "- Calculadora zerada",
        6 => "- Valor atual",
        7 => "- Definir valor da calculadora",
        _ => "- Input desconhecido"
    };

    Console.WriteLine(message);

    calcValue = (operationType is 7)
        ? GetDoubleInput()
        : Calculate(operationType, 0, calcValue);
}

static double GetMathematicalInput(int operationType)
{
    var message = operationType switch
    {
        1 => "- Somar por",
        2 => "- Subtrair por",
        3 => "- Multiplicar por",
        4 => "- Dividir por",
        _ => "- Operação desconhecida"
    };

    Console.WriteLine(message);

    return GetDoubleInput();
}

static int GetIntInput()
{
    var input = 0;

    do
    {
        Console.Write("> ");
    } while (!int.TryParse(Console.ReadLine(), out input));

    return input;
}

static double GetDoubleInput()
{
    var input = 0.0;

    do
    {
        Console.Write("> ");
    } while (!double.TryParse(Console.ReadLine(), out input));

    return input;
}

static double Calculate(int input, double value, double calcValue)
{
    return input switch
    {
        1 => SomarValor(value, calcValue),
        2 => SubtrairValor(value, calcValue),
        3 => MultiplicarValor(value, calcValue),
        4 => DividirValor(value, calcValue),
        5 => ZerarResultado(ref calcValue),
        7 => DefinirResultadoDaUltimaOperacaoMatematica(value, ref calcValue),
        _ => calcValue
    };
}

static bool IsMathematicalOperation(int operationType)
    => operationType is > 0 and < 5;

static double SomarValor(double valor, double calcValue)
    => calcValue + valor;

static double SubtrairValor(double valor, double calcValue)
    => calcValue - valor;

static double DividirValor(double valor, double calcValue)
{
    return (valor is 0)
        ? throw new DivideByZeroException("CodError001 - Divisão por 0 não é possível.")
        : calcValue / valor;
}

static double MultiplicarValor(double valor, double calcValue)
    => calcValue * valor;

static double ZerarResultado(ref double calcValue)
    => calcValue = 0;

//double PegarResultadoDaUltimaOperacaoMatematica()
//    => resultado;

static double DefinirResultadoDaUltimaOperacaoMatematica(double newValue, ref double calcValue)
    => calcValue = newValue;

