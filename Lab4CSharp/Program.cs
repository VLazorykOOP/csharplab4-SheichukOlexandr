using System;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Оберіть завдання:");
            Console.WriteLine("1. Розширення класу Trapeze");
            Console.WriteLine("2. Клас VectorFloat");
            Console.WriteLine("0. Вийти");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 2)
            {
                Console.WriteLine("Некоректний ввід. Будь ласка, введіть число від 0 до 2.");
            }

            if (choice == 0)
                break;

            switch (choice)
            {
                case 1:
                    Task1();
                    break;
                case 2:
                    Task2();
                    break;
            }
        }
    }

    static void Task1()
    {
        Trapeze[] trapezes = {
            new Trapeze(3, 6, 4, 1),
            new Trapeze(5, 8, 6, 2),
            new Trapeze(2, 4, 3, 1),
            new Trapeze(6, 10, 5, 2),
            new Trapeze(5, 5, 5, 5)
        };

        foreach (var trapeze in trapezes)
        {
            Console.WriteLine("Інформація про трапецію:");
            trapeze.ShowLengths();
            Console.WriteLine($"Площа: {trapeze.CalculateArea()}");
            Console.WriteLine($"Периметр: {trapeze.CalculatePerimeter()}");
            Console.WriteLine($"Чи є квадратом: {trapeze.IsSquare}");
            Console.WriteLine();
        }
    }

    static void Task2()
    {
        VectorFloat v1 = new VectorFloat(3, 2.5f);
        VectorFloat v2 = new VectorFloat(3, 3.5f);

        VectorFloat sum = v1 + v2;
        Console.WriteLine("Сума двох векторів:");
        sum.PrintVector();

        VectorFloat v3 = v1 + 2;
        Console.WriteLine("Додавання скаляра до вектора:");
        v3.PrintVector();

        // Тестування інших операцій тут
        // ...

        Console.WriteLine($"Кількість створених векторів: {VectorFloat.CountVectors()}");

        // Очищення пам'яті тут
        // ...
    }
}

class Trapeze
{
    private int a;
    private int b;
    private int h;
    private int color;

    public Trapeze(int a, int b, int h, int color)
    {
        this.a = a;
        this.b = b;
        this.h = h;
        this.color = color;
    }

    public int this[int index]
    {
        get
        {
            switch (index)
            {
                case 0: return a;
                case 1: return b;
                case 2: return h;
                case 3: return color;
                default:
                    Console.WriteLine("Помилка: невірний індекс.");
                    return -1;
            }
        }
        set
        {
            switch (index)
            {
                case 0: a = value; break;
                case 1: b = value; break;
                case 2: h = value; break;
                case 3: color = value; break;
                default:
                    Console.WriteLine("Помилка: невірний індекс.");
                    break;
            }
        }
    }

    public static Trapeze operator ++(Trapeze t)
    {
        t.a++;
        t.b++;
        return t;
    }

    public static Trapeze operator --(Trapeze t)
    {
        t.a--;
        t.b--;
        return t;
    }

    public static bool operator ==(Trapeze t1, Trapeze t2)
    {
        return t1.a == t2.a && t1.b == t2.b && t1.h == t2.h && t1.color == t2.color;
    }

    public static bool operator !=(Trapeze t1, Trapeze t2)
    {
        return !(t1 == t2);
    }

    public static bool operator >(Trapeze t1, Trapeze t2)
    {
        return t1.CalculateArea() > t2.CalculateArea();
    }

    public static bool operator <(Trapeze t1, Trapeze t2)
    {
        return t1.CalculateArea() < t2.CalculateArea();
    }

    public static bool operator >=(Trapeze t1, Trapeze t2)
    {
        return t1.CalculateArea() >= t2.CalculateArea();
    }

    public static bool operator <=(Trapeze t1, Trapeze t2)
    {
        return t1.CalculateArea() <= t2.CalculateArea();
    }

    public static Trapeze operator *(Trapeze t, int scalar)
    {
        t.a *= scalar;
        t.h *= scalar;
        return t;
    }

    public static explicit operator string(Trapeze t)
    {
        return $"Trapeze with sides: {t.a}, {t.b}, height: {t.h}, color: {t.color}";
    }

    public void ShowLengths()
    {
        Console.WriteLine($"Основи: {a} і {b}, Висота: {h}, Колір: {color}");
    }

    public int CalculatePerimeter()
    {
        return a + b + 2 * h;
    }

    public double CalculateArea()
    {
        return (a + b) * h / 2.0;
    }

    public bool IsSquare => a == b;
}

class VectorFloat
{
    protected float[] FArray;
    protected uint num;
    protected int codeError;
    protected static uint num_vec;

    public uint Size => num;

    public int CodeError
    {
        get => codeError;
        set => codeError = value;
    }

    public float this[int index]
    {
        get
        {
            if (index < 0 || index >= num)
            {
                codeError = -1;
                return 0;
            }
            codeError = 0;
            return FArray[index];
        }
        set
        {
            if (index >= 0 && index < num)
            {
                FArray[index] = value;
                codeError = 0;
            }
            else
            {
                codeError = -1;
            }
        }
    }

    public VectorFloat()
    {
        num = 1;
        FArray = new float[num];
        num_vec++;
    }

    public VectorFloat(uint size)
    {
        num = size;
        FArray = new float[num];
        num_vec++;
    }

    public VectorFloat(uint size, float initValue) : this(size)
    {
        for (int i = 0; i < num; i++)
        {
            FArray[i] = initValue;
        }
    }

    ~VectorFloat()
    {
        Console.WriteLine("Вектор видалений");
    }

    public void InputVector()
    {
        Console.WriteLine($"Введіть {num} елементів вектора:");
        for (int i = 0; i < num; i++)
        {
            Console.Write($"Елемент {i + 1}: ");
            if (!float.TryParse(Console.ReadLine(), out FArray[i]))
            {
                Console.WriteLine("Некоректний ввід. Введіть дійсне число.");
                i--;
            }
        }
    }

    public void PrintVector()
    {
        Console.WriteLine("Елементи вектора:");
        for (int i = 0; i < num; i++)
        {
            Console.WriteLine($"Елемент {i + 1}: {FArray[i]}");
        }
    }

    public static uint CountVectors()
    {
        return num_vec;
    }

    public static VectorFloat operator ++(VectorFloat v)
    {
        for (int i = 0; i < v.num; i++)
        {
            v.FArray[i]++;
        }
        return v;
    }

    public static VectorFloat operator --(VectorFloat v)
    {
        for (int i = 0; i < v.num; i++)
        {
            v.FArray[i]--;
        }
        return v;
    }

    public static bool operator ==(VectorFloat v1, VectorFloat v2)
    {
        if (v1.Size != v2.Size)
            return false;
        for (int i = 0; i < v1.Size; i++)
        {
            if (v1[i] != v2[i])
                return false;
        }
        return true;
    }

    public static bool operator !=(VectorFloat v1, VectorFloat v2)
    {
        return !(v1 == v2);
    }

    public static bool operator >(VectorFloat v1, VectorFloat v2)
    {
        if (v1.Size != v2.Size)
            return false;
        for (int i = 0; i < v1.Size; i++)
        {
            if (v1[i] <= v2[i])
                return false;
        }
        return true;
    }

    public static bool operator <(VectorFloat v1, VectorFloat v2)
    {
        if (v1.Size != v2.Size)
            return false;
        for (int i = 0; i < v1.Size; i++)
        {
            if (v1[i] >= v2[i])
                return false;
        }
        return true;
    }

    public static bool operator >=(VectorFloat v1, VectorFloat v2)
    {
        if (v1.Size != v2.Size)
            return false;
        for (int i = 0; i < v1.Size; i++)
        {
            if (v1[i] < v2[i])
                return false;
        }
        return true;
    }

    public static bool operator <=(VectorFloat v1, VectorFloat v2)
    {
        if (v1.Size != v2.Size)
            return false;
        for (int i = 0; i < v1.Size; i++)
        {
            if (v1[i] > v2[i])
                return false;
        }
        return true;
    }

    public static VectorFloat operator +(VectorFloat v1, VectorFloat v2)
    {
        int maxSize = Math.Max((int)v1.Size, (int)v2.Size);
        VectorFloat result = new VectorFloat((uint)maxSize);
        for (int i = 0; i < maxSize; i++)
        {
            float val1 = i < v1.Size ? v1[i] : 0;
            float val2 = i < v2.Size ? v2[i] : 0;
            result[i] = val1 + val2;
        }
        return result;
    }

    public static VectorFloat operator +(VectorFloat v, float scalar)
    {
        VectorFloat result = new VectorFloat(v.Size);
        for (int i = 0; i < v.Size; i++)
        {
            result[i] = v[i] + scalar;
        }
        return result;
    }

    public static VectorFloat operator -(VectorFloat v1, VectorFloat v2)
    {
        int maxSize = Math.Max((int)v1.Size, (int)v2.Size);
        VectorFloat result = new VectorFloat((uint)maxSize);
        for (int i = 0; i < maxSize; i++)
        {
            float val1 = i < v1.Size ? v1[i] : 0;
            float val2 = i < v2.Size ? v2[i] : 0;
            result[i] = val1 - val2;
        }
        return result;
    }

    public static VectorFloat operator -(VectorFloat v, float scalar)
    {
        VectorFloat result = new VectorFloat(v.Size);
        for (int i = 0; i < v.Size; i++)
        {
            result[i] = v[i] - scalar;
        }
        return result;
    }

    public static VectorFloat operator *(VectorFloat v1, VectorFloat v2)
    {
        int maxSize = Math.Max((int)v1.Size, (int)v2.Size);
        VectorFloat result = new VectorFloat((uint)maxSize);
        for (int i = 0; i < maxSize; i++)
        {
            float val1 = i < v1.Size ? v1[i] : 0;
            float val2 = i < v2.Size ? v2[i] : 0;
            result[i] = val1 * val2;
        }
        return result;
    }

    public static VectorFloat operator *(VectorFloat v, float scalar)
    {
        VectorFloat result = new VectorFloat(v.Size);
        for (int i = 0; i < v.Size; i++)
        {
            result[i] = v[i] * scalar;
        }
        return result;
    }

    public static VectorFloat operator /(VectorFloat v1, VectorFloat v2)
    {
        int maxSize = Math.Max((int)v1.Size, (int)v2.Size);
        VectorFloat result = new VectorFloat((uint)maxSize);
        for (int i = 0; i < maxSize; i++)
        {
            float val1 = i < v1.Size ? v1[i] : 0;
            float val2 = i < v2.Size ? v2[i] : 0;
            if (val2 == 0)
            {
                Console.WriteLine("Помилка: ділення на нуль.");
                return null;
            }
            result[i] = val1 / val2;
        }
        return result;
    }

    public static VectorFloat operator /(VectorFloat v, float scalar)
    {
        if (scalar == 0)
        {
            Console.WriteLine("Помилка: ділення на нуль.");
            return null;
        }
        VectorFloat result = new VectorFloat(v.Size);
        for (int i = 0; i < v.Size; i++)
        {
            result[i] = v[i] / scalar;
        }
        return result;
    }

    public static VectorFloat operator %(VectorFloat v1, VectorFloat v2)
    {
        int maxSize = Math.Max((int)v1.Size, (int)v2.Size);
        VectorFloat result = new VectorFloat((uint)maxSize);
        for (int i = 0; i < maxSize; i++)
        {
            float val1 = i < v1.Size ? v1[i] : 0;
            float val2 = i < v2.Size ? v2[i] : 0;
            if (val2 == 0)
            {
                Console.WriteLine("Помилка: ділення на нуль.");
                return null;
            }
            result[i] = val1 % val2;
        }
        return result;
    }

    public static VectorFloat operator %(VectorFloat v, float scalar)
    {
        if (scalar == 0)
        {
            Console.WriteLine("Помилка: ділення на нуль.");
            return null;
        }
        VectorFloat result = new VectorFloat(v.Size);
        for (int i = 0; i < v.Size; i++)
        {
            result[i] = v[i] % scalar;
        }
        return result;
    }

    // Додайте решту перевантажень операцій згідно з умовою завдання

    // Наприклад, перевантаження операторів &, |, ^, >>, <<, ==, !=, >, >=, <, <=
}
