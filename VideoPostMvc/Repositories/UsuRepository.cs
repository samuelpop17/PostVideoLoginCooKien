using VideoPostMvc.Data;
using VideoPostMvc.Models;

namespace VideoPostMvc.Repositories
{
    public class UsuRepository
    {
        private UsuContext context;

        public UsuRepository(UsuContext context)
        {
            this.context = context;
        }

        public Usuario LogInUsuario(string email, string contraseña)
        {
            // Buscar el usuario por correo electrónico
            Usuario usuario = this.context.Usuarios.SingleOrDefault(x => x.Email == email);

            // Si no se encuentra el usuario, devolver null
            if (usuario == null)
            {
                return null;
            }
            else
            {
                // Comparar la contraseña proporcionada con la almacenada en la base de datos
                if (usuario.Contraseña == contraseña)
                {
                    // Contraseña correcta, devolver el usuario
                    return usuario;
                }
                else
                {
                    // Contraseña incorrecta
                    return null;
                }
            }
        }
    }
}
