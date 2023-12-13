using Microsoft.EntityFrameworkCore;
using ProjetWeb3Bourse.Data;

namespace ProjetWeb3Bourse.Models;
    public static class SeedData {

        public static void Initialize(IServiceProvider serviceProvider) {
            using (var context = new BourseContext(
                serviceProvider.GetRequiredService<DbContextOptions<BourseContext>>())) {

            if (context == null || context.Bourse == null) { 
                throw new ArgumentNullException("Null RazorPagesBourseContext");
            }

            if (context.Bourse.Any()) {
                return;
            }

            context.Bourse.AddRange(
                    new Bourse {
                        nom = "Bitcoin",
                        valeur = 56577.51,
                        variation = 1.05
                    },

                    new Bourse {
                        nom = "Ethereum",
                        valeur = 3030.30,
                        variation = 0.32
                    }
                );

            context.Evenement.AddRange(
                new Evenement {
                    bourseId = 1,
                    date = DateTime.Now,
                    heure = DateTime.Now,
                    valeur = 56578.51,
                    variation = 1.01
                },

                new Evenement {
                    bourseId = 2,
                    date = DateTime.Now,
                    heure = DateTime.Now,
                    valeur = 3000.00,
                    variation = -1.1
                }

            );
            context.SaveChanges();
        }
    }

}
