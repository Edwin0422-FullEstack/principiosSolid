namespace solidexample
{
    // programa principal
    class program
    {
        static void Main(string[] args)
        {
            // ejemplo de cada principio solid
            // s: single responsibility principle (srp)
            // o: open/closed principle (opc)
            // l: liskov substitution principle (lsp)
            // i: interface segregation principle (isp)
            // d: dependency inversion principle (dip)

            Console.WriteLine("ejemplos solid en .net 9");

            // srp: una clase debe tener una sola razon para cambiar
            var calculadora = new calculadorasrp();
            var resultado = calculadora.sumar(5, 3);
            Console.WriteLine($"srp - suma: {resultado}");

            // opc: abierto para extension, cerrado para modificacion
            var circulo = new circuloopc(5);
            var rectangulo = new rectanguloopc(4, 6);
            var renderizador = new renderizadoropc();
            renderizador.dibujar(circulo);
            renderizador.dibujar(rectangulo);

            // lsp: los objetos de una clase base deben poder ser reemplazados por objetos de sus clases derivadas
            avelsp[] aves = { new pajarolsp(), new pinguinolsp() };
            foreach (var ave in aves)
            {
                ave.moverse();
            }

            // isp: los clientes no deben depender de interfaces que no usan
            var impresora = new impresoramultifuncionisp();
            impresora.imprimir("documento isp");

            // dip: los modulos de alto nivel no deben depender de los de bajo nivel, sino de abstracciones
            var servicio = new servicioreportedip(new generadorpdfdip());
            servicio.generarreporte("datos dip");

            Console.WriteLine("fin de ejemplos solid");
        }
    }

    // ==================== s: single responsibility principle ====================
    // una clase debe tener solo una responsabilidad
    public class calculadorasrp
    {
        // responsabilidad: realizar calculos matematicos
        public int sumar(int a, int b)
        {
            return a + b;
        }
    }

    // ==================== o: open/closed principle ====================
    // entidades deben estar abiertas para extension pero cerradas para modificacion
    public abstract class formaopc
    {
        public abstract double area();
    }

    public class circuloopc : formaopc
    {
        public double radio { get; set; }

        public circuloopc(double radio)
        {
            this.radio = radio;
        }

        public override double area()
        {
            return Math.PI * radio * radio;
        }
    }

    public class rectanguloopc : formaopc
    {
        public double ancho { get; set; }
        public double alto { get; set; }

        public rectanguloopc(double ancho, double alto)
        {
            this.ancho = ancho;
            this.alto = alto;
        }

        public override double area()
        {
            return ancho * alto;
        }
    }

    public class renderizadoropc
    {
        // se extiende agregando nuevas formas sin modificar esta clase
        public void dibujar(formaopc forma)
        {
            Console.WriteLine($"opc - dibujando forma con area: {forma.area()}");
        }
    }

    // ==================== l: liskov substitution principle ====================
    // las subclases deben ser sustituibles por sus clases base
    public abstract class avelsp
    {
        public abstract void moverse();
    }

    public class pajarolsp : avelsp
    {
        public override void moverse()
        {
            Console.WriteLine("lsp - el pajaro vuela");
        }
    }

    public class pinguinolsp : avelsp
    {
        public override void moverse()
        {
            Console.WriteLine("lsp - el pinguino nada");
        }
    }

    // ==================== i: interface segregation principle ====================
    // interfaces pequenas y especificas en lugar de una grande
    public interface iimprimirisp
    {
        void imprimir(string contenido);
    }

    public interface iescanearisp
    {
        void escanear(string documento);
    }

    // esta clase solo implementa lo que necesita
    public class impresoramultifuncionisp : iimprimirisp
    {
        public void imprimir(string contenido)
        {
            Console.WriteLine($"isp - imprimiendo: {contenido}");
        }
    }

    // ==================== d: dependency inversion principle ====================
    // depender de abstracciones, no de implementaciones concretas
    public interface igeneradorreportedip
    {
        void generar(string datos);
    }

    public class generadorpdfdip : igeneradorreportedip
    {
        public void generar(string datos)
        {
            Console.WriteLine($"dip - generando pdf con: {datos}");
        }
    }

    public class generadorexceldip : igeneradorreportedip
    {
        public void generar(string datos)
        {
            Console.WriteLine($"dip - generando excel con: {datos}");
        }
    }

    // alto nivel depende de abstraccion, no de concreto
    public class servicioreportedip
    {
        private readonly igeneradorreportedip _generador;

        public servicioreportedip(igeneradorreportedip generador)
        {
            _generador = generador;
        }

        public void generarreporte(string datos)
        {
            _generador.generar(datos);
        }
    }
}