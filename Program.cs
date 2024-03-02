using Microsoft.AspNetCore.DataProtection.KeyManagement;
using PoliziaApp.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PoliziaApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Visualizza}/{action=Index}/{id?}");

            app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inserisci}/{action=Index}/{id?}");


            app.Run();
        }
    }
}
// ==========================================================================================
// -------------------------SCRIPT PER LA CREAZIONE DELLE TABELLE----------------------------
// ==========================================================================================


// ==========================================================================================
// ----------------------------CREAZIONE TABELLA NOMINATIVI----------------------------------
// ==========================================================================================

//USE[PoliziaMunicipale]
//GO

///****** Object:  Table [dbo].[Anagrafica]    Script Date: 02/03/2024 12:27:34 ******/
//SET ANSI_NULLS ON
//GO

//SET QUOTED_IDENTIFIER ON
//GO

//CREATE TABLE [dbo].[Anagrafica](
//    [id][int] IDENTITY(1, 1) NOT NULL,
//    [Nome][nvarchar](32) NOT NULL,
//    [Cognome][nvarchar](32) NOT NULL,
//    [Indirizzo][varchar](96) NOT NULL,
//    [Città][varchar](50) NOT NULL,
//    [CAP][char](5) NOT NULL,
//    [CodiceFiscale][char](16) NOT NULL,
// CONSTRAINT[PK_Anagrafica] PRIMARY KEY CLUSTERED
//(
//    [id] ASC
//)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON[PRIMARY]
//) ON[PRIMARY]
//GO

// ==========================================================================================
// ------------------------CREAZIONE TABELLA VERBALI-----------------------------------------
// ==========================================================================================


//USE[PoliziaMunicipale]
//GO

///****** Object:  Table [dbo].[Verbali]    Script Date: 02/03/2024 12:28:46 ******/
//SET ANSI_NULLS ON
//GO

//SET QUOTED_IDENTIFIER ON
//GO

//CREATE TABLE [dbo].[Verbali](
//    [id][int] IDENTITY(1, 1) NOT NULL,
//[DataViolazione][date] NOT NULL,
//    [IndirizzoViolazione][varchar](96) NOT NULL,
//    [DataTrascrizioneVerbale][date] NOT NULL,
//    [Importo][money] NOT NULL,
//    [DecurtamentoPunti][int] NOT NULL,
//    [TipoViolazione][varchar](50)  NOT NULL,
//    [Nominativo][int] NOT NULL,
//    [NominativoAgente][varchar](50) NOT NULL,
// CONSTRAINT[PK_Verbali] PRIMARY KEY CLUSTERED
//(
//    [id] ASC
//)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON[PRIMARY]
//) ON[PRIMARY]
//GO

//ALTER TABLE [dbo].[Verbali]  WITH CHECK ADD  CONSTRAINT [FK_Verbali_Anagrafica] FOREIGN KEY([Nominativo])
//REFERENCES[dbo].[Anagrafica]([id])
//GO

//ALTER TABLE [dbo].[Verbali] CHECK CONSTRAINT[FK_Verbali_Anagrafica]
//GO

// ==========================================================================================
// ---------------------------------------------NOTE-----------------------------------------
// ==========================================================================================

// Per riempire il database / i form ho usato un'estensione di firefox chiamata Fake Data, consiglio di fare lo stesso 
// ad eccezione dei dati per cui non è possibile (CAP e codice fiscale) che vanno inseriti a mano

