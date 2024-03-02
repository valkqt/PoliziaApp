using Microsoft.AspNetCore.Mvc;
using PoliziaApp.Models;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Net;

namespace PoliziaApp.Controllers
{
    public class HomeController : Controller
    {

        private string connectionString = "Server=Mephisto\\SQLEXPRESS; Initial Catalog=PoliziaMunicipale; Integrated Security=true; TrustServerCertificate=True";
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            SqlConnection con = new SqlConnection(connectionString);
            List<Nominativo> anagrafe = new List<Nominativo>();

            try
            {
                con.Open();
                SqlCommand select = new SqlCommand("select * from Anagrafica", con);
                SqlDataReader reader = select.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Nominativo persona = new Nominativo();

                        persona.Id = (int)reader["id"];
                        persona.Nome = (string)reader["Nome"];
                        persona.Cognome = (string)reader["Cognome"];
                        persona.Indirizzo = (string)reader["Indirizzo"];
                        persona.Città = (string)reader["Città"];
                        persona.CAP = reader["CAP"].ToString();
                        persona.CodiceFiscale = (string)reader["CodiceFiscale"];

                        anagrafe.Add(persona);
                    }
                    reader.Close();
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = true;
                TempData["exception"] = ex.Message;


                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return View(anagrafe);
        }

        [HttpPost]
        public IActionResult Add(string first_name, string last_name, string address, string cap, string city, string cf)
        {
            SqlConnection con = new SqlConnection(connectionString);
            int codice;
            try
            {
                if (!int.TryParse(cap, out codice))
                {
                    throw new Exception("Inserire un codice postale valido.");
                }

                string pattern = "^([A-Z]{6}[0-9LMNPQRSTUV]{2}[ABCDEHLMPRST]{1}[0-9LMNPQRSTUV]{2}[A-Z]{1}[0-9LMNPQRSTUV]{3}[A-Z]{1})$|([0-9]{11})$";
                Regex rg = new Regex(pattern);
                if (!rg.IsMatch(cf))
                {
                    throw new Exception("Inserire un Codice Fiscale valido.");
                }

                con.Open();
                SqlCommand insert = new SqlCommand("insert into Anagrafica (Nome, Cognome, Indirizzo, Città, CAP, CodiceFiscale)" +
                    "values (@name, @surname, @address, @city, @cap, @cf)", con);
                insert.Parameters.AddWithValue("@name", first_name);
                insert.Parameters.AddWithValue("@surname", last_name);
                insert.Parameters.AddWithValue("@address", address);
                insert.Parameters.AddWithValue("@city", city);
                insert.Parameters.AddWithValue("@cap", cap);
                insert.Parameters.AddWithValue("@cf", cf);
                insert.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                TempData["error"] = true;
                TempData["exception"] = ex.Message;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }

            TempData["success"] = true;
            TempData["infomsg"] = "Inserimento avvenuto con successo.";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Verbale(int? id, string DataViolazione, string address, double Importo, int DecurtamentoPunti, string TipoViolazione, int IdNominativo, string fullName)
        {
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                con.Open();
                SqlCommand insert = new SqlCommand("insert into Verbali " +
                    "(DataViolazione, IndirizzoViolazione, DataTrascrizioneVerbale, Importo, DecurtamentoPunti, TipoViolazione, Nominativo, NominativoAgente)" +
                    "values (@DataViolazione, @IndirizzoViolazione, @DataTrascrizioneVerbale, @Importo, @DecurtamentoPunti, @TipoViolazione, @Nominativo, @NominativoAgente)", con);
                insert.Parameters.AddWithValue("@DataViolazione", DataViolazione);
                insert.Parameters.AddWithValue("@IndirizzoViolazione", address);
                insert.Parameters.AddWithValue("@DataTrascrizioneVerbale", DateTime.Now);
                insert.Parameters.AddWithValue("@Importo", Importo);
                insert.Parameters.AddWithValue("@DecurtamentoPunti", DecurtamentoPunti);
                insert.Parameters.AddWithValue("@TipoViolazione", TipoViolazione);
                insert.Parameters.AddWithValue("@Nominativo", IdNominativo);
                insert.Parameters.AddWithValue("@NominativoAgente", fullName);

                insert.ExecuteNonQuery();
                TempData["success"] = true;
                TempData["infomsg"] = "Inserimento avvenuto con successo.";


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                TempData["error"] = true;

                TempData["exception"] = ex.Message;

            }
            finally
            {
                con.Close();
            }



            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
