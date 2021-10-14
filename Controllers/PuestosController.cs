using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using web_api_clientes.Models;
namespace web_api_clientes.Controllers{
    [Route("api/[controller]")]
    public class PuestosController : Controller {
        private Conexion dbConexion;
        public  PuestosController(){
            dbConexion = Conectar.Create(); 
        }

        //Leer
        [HttpGet]
         public ActionResult Get(){
            return Ok(dbConexion.Puestos.ToArray());
        }
        //Leer segun id que tenga en la base de datos
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id){
            var puestos = await dbConexion.Puestos.FindAsync(id);
            if (puestos !=null){
                return Ok(puestos);
            }else{
                return NotFound();
            }
        }

        //agregar
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Puestos puestos){
            if(ModelState.IsValid){
                dbConexion.Puestos.Add(puestos);
                await dbConexion.SaveChangesAsync();
                return Ok();           
            }else{
                return BadRequest();
            }
        }

        //modificar
        public async Task<ActionResult> Put([FromBody] Puestos puestos) {
            var v_puestos = dbConexion.Puestos.SingleOrDefault(a => a.id_puesto == puestos.id_puesto);
            if(v_puestos !=null && ModelState.IsValid){
                dbConexion.Entry(v_puestos).CurrentValues.SetValues(puestos);
                await dbConexion.SaveChangesAsync();
                return Ok();           
            }else{
                return BadRequest();
            }
        }

        //eliminar
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) {
            var v_puestos = dbConexion.Puestos.SingleOrDefault(a => a.id_puesto == id);
            if (v_puestos !=null){
                dbConexion.Puestos.Remove(v_puestos);
                await dbConexion.SaveChangesAsync();
                return Ok();
            }else{
                return NotFound();
            }

        }

    }
    
}