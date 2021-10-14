using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using web_api_clientes.Models;
namespace web_api_clientes.Controllers{
    [Route("api/[controller]")]
    public class EmpleadosController : Controller {
        private Conexion dbConexion;
        public  EmpleadosController(){
            dbConexion = Conectar.Create(); 
        }

        //Leer
        [HttpGet]
         public ActionResult Get(){
            return Ok(dbConexion.Empleados.ToArray());
        }
        //Leer segun id que tenga en la base de datos
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id){
            var empleados = await dbConexion.Empleados.FindAsync(id);
            if (empleados !=null){
                return Ok(empleados);
            }else{
                return NotFound();
            }
        }

        //agregar
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Empleados empleados){
            if(ModelState.IsValid){
                dbConexion.Empleados.Add(empleados);
                await dbConexion.SaveChangesAsync();
                return Ok();           
            }else{
                return BadRequest();
            }
        }

        //modificar
        public async Task<ActionResult> Put([FromBody] Empleados empleados) {
            var v_empleados = dbConexion.Empleados.SingleOrDefault(a => a.id_empleado == empleados.id_empleado);
            if(v_empleados !=null && ModelState.IsValid){
                dbConexion.Entry(v_empleados).CurrentValues.SetValues(empleados);
                await dbConexion.SaveChangesAsync();
                return Ok();           
            }else{
                return BadRequest();
            }
        }

        //eliminar
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) {
            var v_empleados = dbConexion.Empleados.SingleOrDefault(a => a.id_empleado == id);
            if (v_empleados !=null){
                dbConexion.Empleados.Remove(v_empleados);
                await dbConexion.SaveChangesAsync();
                return Ok();
            }else{
                return NotFound();
            }

        }

    }
    
}