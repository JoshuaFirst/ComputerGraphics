struct RGB
{
    public RGB(int r, int g, int b) : this()
    {
        R = r;
        G = g;
        B = b;
    }

    public int R { get; set; }

    public int G { get; set; }

    public int B { get; set; }

    public static RGB operator +(RGB one, RGB other) =>
        new RGB(one.R + other.R, one.G + other.G, one.B + other.B);

    public static RGB operator -(RGB one, RGB other) =>
        new RGB(one.R - other.R, one.G - other.G, one.B - other.B);

    public static RGB operator *(RGB one, int n) =>
        new RGB(one.R * n, one.G * n, one.B * n);

    public static RGB operator /(RGB one, double n) =>
        new RGB((int)(one.R / n), (int)(one.G / n), (int)(one.B / n));
}