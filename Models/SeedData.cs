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
            context.SaveChanges();
        }
    }

}
