using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystemAI
{
    class ProcessBaseConstraints
    {



        public int ColumnConstraints(String key, Object value)
        {

            //in switch call specific functions

            int points = 0;
            switch (key.ToUpper())
            {
                case "STUDENTNUMMER":
                    Logger.DisplaySingleLine(value);
                    break;
                case "PLAATS":
                    // if further than x km -5 (difficult to check so maybe scratch this)
                    break;
                case "NAAM_VOOROPLEIDING":
                    points += PrevEducationIsVWO(value);
                    points += PrevEducationIsTechRelated(value);
                    break;
                case "GESLACHT":
                    points += GenderPoints(value);
                    break;
                case "":
                    if (value.ToString().Length == 4)
                    {
                        // TODO: check for year
                        //is this a year?
                    }
                    //additional check to see wether this is indeed the year of birth
                    break;
                case "WAS_AANWEZIG":
                    points += AttendedToIntakePoints(value);
                    break;
                case "ADVIES":
                    Logger.DisplaySingleLine(" " + value + " ");
                    //currently we do not use this
                    break;
                case "AANTAL_WEIGERINGEN":
                    points += RejectionPoints(value);
                    break;
                case "GEWOGEN_GEMIDDELDE":
                case "KOMT_STUDEREN":
                case "REDEN_STOPPEN":
                    //currently we do not use this
                    break;
                case "VOORKEURSOPLEIDING":
                    points += PreferredStudy(value);
                    break;
                case "COMPETENTIES":
                    points += CompetencePoints(value);
                    break;
                case "CAPACITEITEN":
                    points += CapacityPoints(value);
                    break;
                case "INTR. MOTIVATIE":
                    points += IntrMotivationalPoints(value);
                    break;
                case "EXTR. MOTIVATIE":
                    points += ExtrMotivationalPoints(value);
                    break;
                case "IS_MBO_DEFICIENT":
                    points += DeficientPoints(value);
                    break;
                case "ONDERNEMEN IN COMBINATIE MET STUDIE":
                    points += OwnCompanyPoints(value);
                    break;

            }
          
            return points;
        }

        // NAAM_VOOROPLEIDING
        private int PrevEducationIsVWO(Object p)
        {

            bool result = false;
            String education = p.ToString().ToUpper();
            result = education.Contains("VWO");
            if (result)
            {
                return 10;
            }
            return 1;
        }

        // NAAM_VOOROPLEIDING
        private int PrevEducationIsTechRelated(Object p)
        {
            String education = p.ToString().ToUpper();
      
            //todo check for other unforseen outcomes -> lazy
            // result is true, when loop stops because value is found, then result wont be set to false
            // if loop hits break then no occurrances found and thus result is false

            List<bool> bools = new List<bool>();

            bools.Add(education.Contains("APPLICATIE"));
            bools.Add(education.Contains("ICT"));
            bools.Add(education.Contains("ONTWIKKELAAR"));
            bools.Add(education.Contains("INFORMATICA"));
            bools.Add(education.Contains("TECH"));
            bools.Add(education.Contains("NETWERK"));
            Logger.Log(bools);
            if (bools.Contains(true))
            {
                return 10;
            }
            return 1;


        }

        // GESLACHT
        private int GenderPoints(Object o)
        {
            if (o.ToString() == "v" || o.ToString() == "f")
            {
                return 5;
            }
            else
            {
                return 0;
            }
        }

        // WAS_AANWEZIG
        private int AttendedToIntakePoints(Object o)
        {
           
            if (o.ToString().ToUpper().Contains("JA")
                || o.ToString().ToUpper().Contains("TRUE"))
            {
                Logger.Log(o);
                return 1;
            }
            else
            {
                return -25;
            }          
        }

        // AANTAL_WEIGERINGEN
        private int RejectionPoints(Object o)
        {
            //retuns x times the reject value as a negative.
            //String str = o.ToString();
            int rejects =  Convert.ToInt32(o);
            return -(rejects * 25);
        }

        // VOORKEURSOPLEIDING
        private int PreferredStudy(Object value)
        {

            String education = value.ToString().ToUpper();

            //todo check for other unforseen outcomes -> lazy
            // result is true, when loop stops because value is found, then result wont be set to false
            // if loop hits break then no occurrances found and thus result is false

            List<bool> bools = new List<bool>();
            bools.Add(education.Contains("APPLICATIE"));
            bools.Add(education.Contains("ICT"));
            bools.Add(education.Contains("ONTWIKKELAAR"));
            bools.Add(education.Contains("INFORMATICA"));

            int points = 0;
            if (bools.Contains(true))
            {
                points =10;
            }else if(education == "0")
            {
                points = 1;
            }
            else
            {
                Logger.Log("alternative preffered education " + education);
                points = -10;
            }
            return points;
        }

        // COMPETENTIES
        private int CompetencePoints(Object value)
        {
            int competence = Convert.ToInt32(value);
            int points = 0;
            if(competence >= 7)
            {
                points += competence * 3;
            }else if( competence >= 5)
            {
                points += competence * 2;
            }else if( competence < 5)
            {
                points += competence * 1;
            }
            return points;
        }

        // CAPACITEITEN
        private int CapacityPoints(Object value)
        {
            int capacity = Convert.ToInt32(value);
            double points = 0;
            if (capacity >= 7)
            {
                points += capacity * 3.5;
            }
            else if (capacity >= 5)
            {
                points += capacity * 3;
            }
            else if (capacity < 5)
            {
                points += capacity * 2;
            }

            int p = Convert.ToInt32(points);
            return p;
        }

        // INTR. MOTIVATIE
        private int IntrMotivationalPoints(Object value)
        {
            int motivation = Convert.ToInt32(value);
            double points = 0;
            if (motivation >= 7)
            {
                points += motivation * 5;
            }
            else if (motivation >= 5)
            {
                points += motivation * 4;
            }
            else if (motivation < 5)
            {
                points += motivation * 3;
            }

            int p = Convert.ToInt32(points);
            return p;
        }

        // EXTR. MOTIVATIE
        private int ExtrMotivationalPoints(Object value)
        {
            int motivation = Convert.ToInt32(value);
            double points = 0;
            if (motivation >= 7)
            {
                points += motivation * 3;
            }
            else if (motivation >= 5)
            {
                points += motivation * 2;
            }
            else if (motivation < 5)
            {
                points += motivation * 1;
            }

            int p = Convert.ToInt32(points);
            return p;
        }

        // IS_MBO_DEFICIENT
        private int DeficientPoints(Object o)
        {
            int points = 0;
            if (o.ToString().ToUpper().Contains("JA"))
            {
                // if irrelevant mbo study then lose points because it 
                // will be harder for said student to keep up with HBO learning tempo
                points = -10;
            }
            else if(o.ToString().ToUpper().Contains("NEE"))
            {
                // extra points to seperate chaff from corn
                points = 5;
            }
            return points;
        }

        // ONDERNEMEN IN COMBINATIE MET STUDIE
        private int OwnCompanyPoints(Object o)
        {
            int points = 0;
            if (Convert.ToInt32(o) == 1)
            {
                // when student has own company get extra points because of motivation
                points = 10;
            }
            else
            {
                points = 0;
            }
            return points;
        }
    }
}
