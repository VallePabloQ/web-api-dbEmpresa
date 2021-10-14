using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using web_api_clientes.Models;
namespace web_api_clientes.Controllers{
    [Route("api/[controller]")]
    public class ClientesController : Controller {
        private Conexion dbConexion;
        public  ClientesController(){
            dbConexion = Conectar.Create(); 
        }

        //Leer
        [HttpGet]
         public ActionResult Get(){
            return Ok(dbConexion.Clientes.ToArray());
        }
        //Leer segun id que tenga en la base de datos
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id){
            var clientes = await dbConexion.Clientes.FindAsync(id);
            if (clientes !=null){
                return Ok(clientes);
            }else{
                return NotFound();
            }
        }

        //agregar
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Clientes clientes){
            if(ModelState.IsValid){
                dbConexion.Clientes.Add(clientes);
                await dbConexion.SaveChangesAsync();
                return Ok();           
            }else{
                return BadRequest();
            }
        }

        //modificar
        public async Task<ActionResult> Put([FromBody] Clientes clientes) {
            var v_clientes = dbConexion.Clientes.SingleOrDefault(a => a.id_cliente == clientes.id_cliente);
            if(v_clientes !=null && ModelState.IsValid){
                dbConexion.Entry(v_clientes).CurrentValues.SetValues(clientes);
                await dbConexion.SaveChangesAsync();
                return Ok();           
            }else{
                return BadRequest();
            }
        }

        //eliminar
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) {
            var v_clientes = dbConexion.Clientes.SingleOrDefault(a => a.id_cliente == id);
            if (v_clientes !=null){
                dbConexion.Clientes.Remove(v_clientes);
                await dbConexion.SaveChangesAsync();
                return Ok();
            }else{
                return NotFound();
            }

        }

    }
    
}