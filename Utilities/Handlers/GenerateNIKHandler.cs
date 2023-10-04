using API.Contracts;

namespace API.Utilities.Handlers
{

    public class GenerateNIKHandler
    {
        //method untuk menggenerate nik pada employee
        public static string GenerateNIK(String? lastNik = null)
        {
            if(lastNik == null) {
                return "111111";
                
            }
            
                var nik = Convert.ToInt32(lastNik) + 1;
            

            return nik.ToString("D6");
            
        }
    }
}
