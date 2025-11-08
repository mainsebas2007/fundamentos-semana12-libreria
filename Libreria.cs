using System;
using System.Globalization;
using System.Text;

namespace Semana12_LibreriaArrays
{
    public class Libreria
    {
        private string[] _nombres = new string[0];
        private decimal[] _precios = new decimal[0];

        private readonly CultureInfo _culture = new CultureInfo("es-PE");

        // --- VALIDACIONES ---
        private void ValidarNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del libro no puede ser nulo ni vacío.");
        }

        private void ValidarPrecio(decimal precio)
        {
            if (precio < 0m)
                throw new ArgumentException("El precio no puede ser negativo.");
            if (precio > 1000m)
                throw new ArgumentException("El precio máximo permitido es 1000.");
        }

        private int BuscarIndicePorNombre(string nombre)
        {
            int i = 0;
            while (i < _nombres.Length)
            {
                if (string.Equals(_nombres[i], nombre, StringComparison.OrdinalIgnoreCase))
                    return i;
                i++;
            }
            return -1;
        }

        private bool ExisteNombre(string nombre)
        {
            return BuscarIndicePorNombre(nombre) != -1;
        }

        // --- MÉTODOS CRUD ---
        public void Registrar(string nombre, string precioComoTexto)
        {
            ValidarNombre(nombre);
            if (ExisteNombre(nombre))
                throw new InvalidOperationException("Ya existe un libro con ese nombre.");

            if (string.IsNullOrWhiteSpace(precioComoTexto))
                throw new ArgumentException("El precio no puede ser nulo ni vacío.");

            decimal precio;
            // Permite coma o punto
            string normalizado = precioComoTexto
                .Replace(".", _culture.NumberFormat.NumberDecimalSeparator)
                .Replace(",", _culture.NumberFormat.NumberDecimalSeparator);

            if (!decimal.TryParse(normalizado, NumberStyles.Number, _culture, out precio))
                throw new ArgumentException("El precio debe ser un valor numérico válido.");

            ValidarPrecio(precio);

            // Redimensionar y asignar al último índice manualmente (sin ^1)
            int nuevaLongitud = _nombres.Length + 1;
            Array.Resize(ref _nombres, nuevaLongitud);
            Array.Resize(ref _precios, nuevaLongitud);

            int ultimo = nuevaLongitud - 1;
            _nombres[ultimo] = nombre.Trim();
            _precios[ultimo] = precio;
        }

        public string Mostrar()
        {
            if (_nombres.Length == 0)
                return "No hay libros registrados.";

            string sep = new string('-', 60);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(sep);
            sb.AppendLine(string.Format("{0,3}  {1,-35} {2,10}", "#", "Libro", "Precio"));
            sb.AppendLine(sep);

            int i = 0;
            while (i < _nombres.Length)
            {
                sb.AppendLine(string.Format("{0,3}  {1,-35} {2,10}",
                    i + 1,
                    _nombres[i],
                    _precios[i].ToString("C2", _culture)));
                i++;
            }

            sb.AppendLine(sep);
            return sb.ToString();
        }

        public void Modificar(string nombreActual, string nuevoNombre, string nuevoPrecioTexto)
        {
            int idx = BuscarIndicePorNombre(nombreActual);
            if (idx == -1)
                throw new InvalidOperationException("El libro a modificar no existe.");

            ValidarNombre(nuevoNombre);
            if (!string.Equals(nombreActual, nuevoNombre, StringComparison.OrdinalIgnoreCase) && ExisteNombre(nuevoNombre))
                throw new InvalidOperationException("Ya existe otro libro con el nuevo nombre.");

            if (string.IsNullOrWhiteSpace(nuevoPrecioTexto))
                throw new ArgumentException("El precio no puede ser nulo ni vacío.");

            string normalizado = nuevoPrecioTexto
                .Replace(".", _culture.NumberFormat.NumberDecimalSeparator)
                .Replace(",", _culture.NumberFormat.NumberDecimalSeparator);

            decimal nuevoPrecio;
            if (!decimal.TryParse(normalizado, NumberStyles.Number, _culture, out nuevoPrecio))
                throw new ArgumentException("El precio debe ser un valor numérico válido.");

            ValidarPrecio(nuevoPrecio);

            _nombres[idx] = nuevoNombre.Trim();
            _precios[idx] = nuevoPrecio;
        }

        public void Eliminar(string nombre)
        {
            int idx = BuscarIndicePorNombre(nombre);
            if (idx == -1)
                throw new InvalidOperationException("El libro a eliminar no existe.");

            string[] nuevosNombres = new string[_nombres.Length - 1];
            decimal[] nuevosPrecios = new decimal[_precios.Length - 1];

            int i = 0;
            int k = 0;
            while (i < _nombres.Length)
            {
                if (i != idx)
                {
                    nuevosNombres[k] = _nombres[i];
                    nuevosPrecios[k] = _precios[i];
                    k++;
                }
                i++;
            }

            _nombres = nuevosNombres;
            _precios = nuevosPrecios;
        }
    }
}
