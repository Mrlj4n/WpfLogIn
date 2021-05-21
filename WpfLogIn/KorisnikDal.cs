using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;

namespace WpfLogIn
{
    static class KorisnikDal
    {
        public static List<Korisnik> VratiKorisnika()
        {
            List<Korisnik> listaKorisnika = new List<Korisnik>();

            using (SqlConnection konekcija = new SqlConnection(Konekcija.cnnLogovanjeDb))
            {
                using (SqlCommand komanda = new SqlCommand("SELECT * FROM Korisnik", konekcija))
                {

                    try
                    {
                        SqlDataReader dr = komanda.ExecuteReader();
                        while (dr.Read())
                        {
                            Korisnik k = new Korisnik
                            {
                                KorisnikId = dr.GetInt32(0),
                                KorisnickoIme = dr.GetString(1),
                                Lozinka = dr.GetString(2)
                            };

                            listaKorisnika.Add(k);
                        }
                        return listaKorisnika;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }
        }

        public static int Proveri(Korisnik k)
        {
            using (SqlConnection konekcija = new SqlConnection(Konekcija.cnnLogovanjeDb))
            {
                using (SqlCommand komanda = new SqlCommand("Postoji1", konekcija))
                {
                    komanda.CommandType = System.Data.CommandType.StoredProcedure;
                    try
                    {
                        komanda.Parameters.AddWithValue("@KorisnickoIme", k.KorisnickoIme);
                        komanda.Parameters.AddWithValue("@Lozinka", k.Lozinka);
                        var param = komanda.Parameters.Add("@rez", SqlDbType.Int);
                        param.Direction = ParameterDirection.ReturnValue;
                        konekcija.Open();
                        komanda.ExecuteNonQuery();

                        int rez = (int)param.Value;
                        return rez;
                    }
                    catch (Exception xcp)
                    {

                        MessageBox.Show(xcp.Message);
                        return 0;
                    }
             
                }
            }

        }
    }
}
