// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

int fib(int a, int b, int end) {
    int c = a + b;
    if (c < end)
        return fib(b, c, end);
    Console.WriteLine(c);
    return(c);
};
fib(4, 6, 11);

// int a, b, c, end;
// while c < end {
// c = a + b;
// a = b;
// b = c;
// };
// Console.WriteLine(end);