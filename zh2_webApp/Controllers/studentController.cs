using Microsoft.AspNetCore.Mvc;
using zh2_webApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace zh2_webApp.Controllers
{
    [ApiController]
    public class studentController : ControllerBase
    {

        [HttpGet]
        [Route("/api/minden")]
        public IActionResult Get()
        {

            studentContext sc = new();

            return Ok(sc.Students.ToList().Distinct());
        }

        [HttpGet]
        [Route("/api/1/{stud_Id}")]
        public IActionResult Get(int stud_Id)
        {
            studentContext sc = new();

            var ki = (from x in sc.Students
                     join y in sc.Works
                     on x.StudentId equals y.StudentId
                     where x.StudentId == stud_Id
                     select x).FirstOrDefault();
            return Ok(ki);
        }

        [HttpGet]
        [Route("/api/2/{stud_Nev}")]
        public IActionResult Get(string stud_Nev)
        {
            studentContext sc = new();

            var ki = (from x in sc.Students
                      join y in sc.Works
                      on x.StudentId equals y.StudentId
                      where x.Name.Contains(stud_Nev)
                      select x).FirstOrDefault();
            return Ok(ki);
        }


        [HttpDelete]
        [Route("/api/d/{stud_Id}")]
        public void  Delete(int stud_Id)
        {
            studentContext sc = new();
            var torlendo = (from x in sc.Students
                            where x.StudentId == stud_Id
                            select x).FirstOrDefault();

            try
            {
                sc.Remove(torlendo);
                sc.SaveChanges();
            }
            catch (Exception ex)
            {


            }

        }
    }
}
