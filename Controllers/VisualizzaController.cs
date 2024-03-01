using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PoliziaApp.Models;

namespace PoliziaApp.Controllers
{
    public class VisualizzaController : Controller
    {
        private string connectionString = "Server=Mephisto\\SQLEXPRESS; Initial Catalog=PoliziaMunicipale; Integrated Security=true; TrustServerCertificate=True";

        public IActionResult Index()
        {

            SqlConnection con = new SqlConnection(connectionString);
            List<Verbale> verbali = new List<Verbale>();

            try
            {
                con.Open();
                SqlCommand select = new SqlCommand("select Anagrafica.Nome, Anagrafica.Cognome, * from Verbali " +
                    "inner join Anagrafica on Verbali.Nominativo = Anagrafica.id", con);
                SqlDataReader reader = select.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Verbale verbale = new Verbale();

                        verbale.Id = (int)reader["id"];
                        verbale.DataViolazione = (DateTime)reader["DataViolazione"];
                        verbale.IndirizzoViolazione = (string)reader["IndirizzoViolazione"];
                        verbale.DataTrascrizioneVerbale = (DateTime)reader["DataTrascrizioneVerbale"];
                        verbale.Importo = double.Parse(reader["Importo"].ToString());
                        verbale.DecurtamentoPunti = (int)reader["DecurtamentoPunti"];
                        verbale.TipoViolazione = (string)reader["TipoViolazione"];
                        verbale.IdNominativo = (int)reader["Nominativo"];
                        verbale.Nome = (string)reader["Nome"];
                        verbale.Cognome = (string)reader["Cognome"];

                        verbale.NominativoAgente = (string)reader["NominativoAgente"];


                        verbali.Add(verbale);
                    }
                    reader.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return View(verbali);
        }

        public IActionResult Recent()
        {

            SqlConnection con = new SqlConnection(connectionString);
            List<Verbale> verbali = new List<Verbale>();

            try
            {
                con.Open();
                SqlCommand select = new SqlCommand($"select Anagrafica.Nome, Anagrafica.Cognome, * from Verbali " +
                    $"inner join Anagrafica on Verbali.Nominativo = Anagrafica.id " +
                    $"where DataTrascrizioneVerbale between  DATEADD(MONTH, -1, GETDATE()) and GETDATE();", con);
                SqlDataReader reader = select.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Verbale verbale = new Verbale();

                        verbale.Id = (int)reader["id"];
                        verbale.DataViolazione = (DateTime)reader["DataViolazione"];
                        verbale.IndirizzoViolazione = (string)reader["IndirizzoViolazione"];
                        verbale.DataTrascrizioneVerbale = (DateTime)reader["DataTrascrizioneVerbale"];
                        verbale.Importo = double.Parse(reader["Importo"].ToString());
                        verbale.DecurtamentoPunti = (int)reader["DecurtamentoPunti"];
                        verbale.TipoViolazione = (string)reader["TipoViolazione"];
                        verbale.IdNominativo = (int)reader["Nominativo"];
                        verbale.Nome = (string)reader["Nome"];
                        verbale.Cognome = (string)reader["Cognome"];

                        verbale.NominativoAgente = (string)reader["NominativoAgente"];


                        verbali.Add(verbale);
                    }
                    reader.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return View(verbali);
        }
        public IActionResult GreaterThan10Points()
        {

            SqlConnection con = new SqlConnection(connectionString);
            List<JoinModel> verbali = new List<JoinModel>();

            try
            {
                con.Open();
                SqlCommand select = new SqlCommand($"select Nome, Cognome, DataViolazione, Importo, DecurtamentoPunti from Verbali " +
                    $"inner join Anagrafica on Verbali.Nominativo = Anagrafica.id " +
                    $"where DecurtamentoPunti > 10", con);
                SqlDataReader reader = select.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        JoinModel verbale = new JoinModel();

                        verbale.Nome = (string)reader["Nome"];
                        verbale.Cognome = (string)reader["Cognome"];
                        verbale.Importo = double.Parse(reader["Importo"].ToString());
                        verbale.DataViolazione = (DateTime)reader["DataViolazione"];
                        verbale.DecurtamentoPunti = (int)reader["DecurtamentoPunti"];


                        verbali.Add(verbale);
                    }
                    reader.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return View(verbali);
        }

        public IActionResult GreaterThan400Eur()
        {

            SqlConnection con = new SqlConnection(connectionString);
            List<Verbale> verbali = new List<Verbale>();

            try
            {
                con.Open();
                SqlCommand select = new SqlCommand($"select Nome, Cognome, * from Verbali " +
                    $"inner join Anagrafica on Verbali.Nominativo = Anagrafica.id " +
                    $"where Importo > 400", con);
                SqlDataReader reader = select.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Verbale verbale = new Verbale();

                        verbale.Id = (int)reader["id"];
                        verbale.DataViolazione = (DateTime)reader["DataViolazione"];
                        verbale.IndirizzoViolazione = (string)reader["IndirizzoViolazione"];
                        verbale.DataTrascrizioneVerbale = (DateTime)reader["DataTrascrizioneVerbale"];
                        verbale.Importo = double.Parse(reader["Importo"].ToString());
                        verbale.DecurtamentoPunti = (int)reader["DecurtamentoPunti"];
                        verbale.TipoViolazione = (string)reader["TipoViolazione"];
                        verbale.IdNominativo = (int)reader["Nominativo"];
                        verbale.Nome = (string)reader["Nome"];
                        verbale.Cognome = (string)reader["Cognome"];
                        verbale.NominativoAgente = (string)reader["NominativoAgente"];


                        verbali.Add(verbale);
                    }
                    reader.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return View(verbali);
        }

        public IActionResult ByPerson()
        {

            SqlConnection con = new SqlConnection(connectionString);
            List<VerbaliPerPersona> verbali = new List<VerbaliPerPersona>();

            try
            {
                con.Open();
                SqlCommand select = new SqlCommand($"select Nome, Cognome, count(*) as TotaleVerbali from Verbali " +
                    $"inner join Anagrafica on Verbali.Nominativo = Anagrafica.id group by Nome, Cognome ", con);
                SqlDataReader reader = select.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        VerbaliPerPersona verbale = new VerbaliPerPersona();
                        verbale.TotaleVerbali = (int)reader["TotaleVerbali"];
                        verbale.Nome = (string)reader["Nome"];
                        verbale.Cognome = (string)reader["Cognome"];

                        verbali.Add(verbale);
                    }
                    reader.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return View(verbali);
        }
        public IActionResult PointsByPerson()
        {

            SqlConnection con = new SqlConnection(connectionString);
            List<PuntiPerPersona> verbali = new List<PuntiPerPersona>();

            try
            {
                con.Open();
                SqlCommand select = new SqlCommand($"select Nome, Cognome, sum(DecurtamentoPunti) as PuntiDecurtati from Verbali " +
                    $"inner join Anagrafica on Verbali.Nominativo = Anagrafica.id " +
                    $"group by Nome, Cognome ", con);
                SqlDataReader reader = select.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PuntiPerPersona verbale = new PuntiPerPersona();
                        verbale.Nome = (string)reader["Nome"];
                        verbale.Cognome = (string)reader["Cognome"];
                        verbale.PuntiDecurtati = (int)reader["PuntiDecurtati"];
                        verbali.Add(verbale);
                    }
                    reader.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return View(verbali);
        }

    }
}
