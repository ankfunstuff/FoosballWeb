using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace FoossballPlayars.Services
{
   class EloRatingGoal
    {
       public double Point1 { get; private set; }
       public double Point2 { get; private set; }

       public EloRatingGoal(double currentRating1, double currentRating2, int score1, int score2, int gamesPrPlayer)
       {
           if ((score1 + score2) == 0) score1 = score2 = 1;
           var eloTeam1 = new EloRating(currentRating1, currentRating2, 1, 0, gamesPrPlayer);
           var eloTeam2 = new EloRating(currentRating1, currentRating2, 0, 1, gamesPrPlayer);
           var norm = Math.Abs(score1 - score2) * ((double)Math.Max(score1,score2)/10);
           if (norm < 1) norm = 1; //score1==score2


           Point1 = Math.Round((score1 * eloTeam1.Point1 + score2 * eloTeam2.Point1) / norm); //Goal: 

           //var eloTeam3 = new EloRating(currentRating1, currentRating2, score1, score2);
           // Half: Point1 = Math.Round((((score1 * eloTeam1.Point1 + score2 * eloTeam2.Point1)/ norm) + eloTeam3.Point1)/2);
           // Thirds: Point1 = Math.Round((((score1 * eloTeam1.Point1 + score2 * eloTeam2.Point1)/ norm) + eloTeam3.Point1 + eloTeam3.Point1)/3);
           // Elo: Point1 = eloTeam3.Point1;

           Point2 = -Point1;
       }
    }
}
