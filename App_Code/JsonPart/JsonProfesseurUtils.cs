using Json;
using JsonPart.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonPart
{
    public static class JsonProfesseurUtils
    {

        public static List<Professeur> UpdateJsonProfesseurs(this List<Professeur> professeurs)
        {
            JsonFileUtils.Write(professeurs, typeof(List<Professeur>), "professeurs.json");
            professeurs = JsonFileUtils.Read<List<Professeur>>("professeurs.json");
            return professeurs;
        }

    }
}
