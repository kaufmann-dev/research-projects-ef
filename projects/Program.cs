using System;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using projects.model;

namespace projects {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Project Domain Data Hallo!");
            var factory = new ProjectDbContextFactoryMySql();

            using (var context = factory.CreateDbContext(args)) {
                context.Database.ExecuteSqlRaw("DELETE FROM SUBPROJECTS WHERE 1 = 1");
                context.Database.ExecuteSqlRaw("DELETE FROM FUNDING WHERE 1 = 1");
                context.Database.ExecuteSqlRaw("DELETE FROM PROJECTS WHERE 1 = 1");
                context.Database.ExecuteSqlRaw("DELETE FROM FACILITIES WHERE 1 = 1");
                context.Database.ExecuteSqlRaw("DELETE FROM DEBITORS WHERE 1 = 1");
            }

            var projects = new AProject[] {
                new RequestFundingProject()  { Title = "Finite Elemente", LegalFoundation = ELegalFoundation.P_27, IsSmallProject = false},
                new RequestFundingProject()  { Title = "Transformationen der linearen Algebra", LegalFoundation = ELegalFoundation.P_27, IsSmallProject = true},
                new ResearchFundingProject() { Title = "Elementare Algebra", LegalFoundation = ELegalFoundation.P_28, IsEUFunded = true, IsFFGFunded = true, IsFWFFunded = false},
                new ResearchFundingProject() { Title = "Alora Alor", LegalFoundation = ELegalFoundation.P_26, IsEUFunded = false, IsFFGFunded = true, IsFWFFunded = true}
            };
            
            using (var context = factory.CreateDbContext(args)) {
                context.Projects.AddRange(projects);
                context.SaveChanges();
            }

            var subprojects = new Subproject[] {
                 new Subproject() { Description = "Finite Elemente - Simulation", AppliedResearch = 50, TheoreticalResearch = 20, FocusResearch = 30 },
                 new Subproject() { Description = "Transformationen der linearen Algebra - Lambda Transformation", AppliedResearch = 10, TheoreticalResearch = 50, FocusResearch = 40},
                 new Subproject() { Description = "Transformationen der linearen Algebra - Faklutät", AppliedResearch = 20, TheoreticalResearch = 50, FocusResearch = 30},
                 new Subproject() { Description = "Elementare Algebra - Gaus Transformation", AppliedResearch = 50, TheoreticalResearch = 50, FocusResearch = 0},
                 new Subproject() { Description = "Elementare Algebra - Differenzengleichungen", AppliedResearch = 20, TheoreticalResearch = 50, FocusResearch = 30}
            };

            using (var context = factory.CreateDbContext(args)) {
                context.UpdateRange(projects);

                subprojects[0].Project = projects[0];
                subprojects[1].Project = projects[1];
                subprojects[2].Project = projects[1];
                subprojects[3].Project = projects[2];
                subprojects[4].Project = projects[2];

                context.Subprojects.AddRange(subprojects);
                context.SaveChanges();
            }

            var institutes = new Institute[] {
                new Institute(){FacilityCode = "345.231", FacilityTitle = "Institut für Softwareentwicklung"},
                new Institute(){FacilityCode = "345.232", FacilityTitle = "Institut für Algebra"},
                new Institute(){FacilityCode = "345.233", FacilityTitle = "Institut für Diskrete Mathematik"}
            };

            using (var context = factory.CreateDbContext(args)) {
                context.UpdateRange(subprojects);
                
                subprojects[0].Institute = institutes[0];
                subprojects[1].Institute = institutes[1];
                subprojects[2].Institute = institutes[1];
                subprojects[3].Institute = institutes[2];
                subprojects[4].Institute = institutes[2];
                
                context.Institutes.AddRange(institutes);
                context.SaveChanges();
            }

            var debitors = new Debitor[] {
                new Debitor(){Name = "TU Wien", Description = "TU Wien, University of Technology"},
                new Debitor(){Name = "EU Horizon 2030", Description = "Research Fund European Union"},
                new Debitor(){Name = "HTL Krems", Description = "Höhere technische Lehranstalt"}
            };

            using (var context = factory.CreateDbContext(args)) {
                context.Debitors.AddRange(debitors);
                context.SaveChanges();
            }

            var fundings = new Funding[] {
                new Funding() { Project = projects[0], Debitor = debitors[0], Amount = 50000f},
                new Funding() { Project = projects[1], Debitor = debitors[0], Amount = 340000f},
                new Funding() { Project = projects[1], Debitor = debitors[1], Amount = 430000f},
                new Funding() { Project = projects[2], Debitor = debitors[0], Amount = 210000f},
                new Funding() { Project = projects[2], Debitor = debitors[2], Amount = 230000f},
                new Funding() { Project = projects[3], Debitor = debitors[0], Amount = 450000f},
            };
            
            using (var context = factory.CreateDbContext(args)) {
                context.UpdateRange(projects);
                context.UpdateRange(debitors);
                
                context.Fundings.AddRange(fundings);
                context.SaveChanges();
            }

            // LINQ
            using (var context = factory.CreateDbContext(args)) {
                var projectList =
                    from p in context.Projects
                    where p.Title.Contains("Element")
                    select p;
                foreach (var VARIABLE in projectList)
                {
                    Console.WriteLine(VARIABLE);
                }

                var subprojectList =
                    from p in context.Projects
                    group p by p.LegalFoundation into projectGroup
                    select new
                    {
                        LegalFoundation = projectGroup.Key,
                        projectCount = projectGroup.Count()
                    };
                foreach (var VARIABLE in subprojectList)
                {
                    Console.WriteLine(VARIABLE);
                }
                
                // Finden Sie alle Projekte mit einer LegalFoundation P_27
                var _projectList = from p in context.Projects
                    where p.LegalFoundation == ELegalFoundation.P_27
                    select p;

                foreach (var project in projectList) {
                    Console.WriteLine(project.ToString());
                }

                // Finden Sie alle Projekte die in ihrem Name den Begriff Algebra enthalten. 
                projectList = from p in context.Projects
                    where p.Title.ToLower().Contains("Algebra".Trim().ToLower())
                        select p;

                foreach (var project in projectList) {
                    Console.WriteLine(project.ToString());
                }

                // Finden Sie alle RequestFundingProjekte
               

                foreach (var project in projectList) {
                    Console.WriteLine($"data: {project.ToString()}");
                }
            }

            // Finden Sie alle Projekte denen 2 oder mehrere Subprojekte zugeordnet
            // sind.
            using (var context = factory.CreateDbContext(args))
            {
                

                /*foreach (var project in projectList2) {
                    Console.WriteLine($"group: {project}");
                }*/
            }
            
            // Wieviel Projekte sind den unterschiedlichen LegalFoundations
            // zugeordnet
            using (var context = factory.CreateDbContext(args))
            {
                var queryData = from p in context.Projects
                    group p by p.LegalFoundation
                    into projectGroup
                    select new
                    {
                        Key = projectGroup.Key,
                        Count = projectGroup.Count()
                    };
                
                foreach (var data in queryData) {
                    Console.WriteLine($"key: {data.Key} count: {data.Count}");
                }
            }
            
            // Geben Sie für jedes Institut die Anzahl der umgesetzten Subprojekte aus.
            using (var context = factory.CreateDbContext(args))
            {
                var queryData = from s in context.Subprojects
                    group s by s.InstituteId
                    into subprojectGroup
                    select new
                    {
                        InstituteId = subprojectGroup.Key,
                        SubprojectCount = subprojectGroup.Count()
                    };

                foreach (var item in queryData) {
                    Console.WriteLine($"key: {item.InstituteId} count: {item.SubprojectCount}");    
                }
            }
            
            // Geben Sie für jedes Projekt die Projektförderung aus
            using (var context = factory.CreateDbContext(args))
            {
                var queryData = from f in context.Fundings
                    group f by f.ProjectId
                    into projectGroup
                    select new
                    {
                        ProjectId = projectGroup.Key,
                        FundingAmount = projectGroup.Sum(f => f.Amount)
                    };

                foreach (var item in queryData) {
                    Console.WriteLine($"projectId: {item.ProjectId} fundingAmount: {item.FundingAmount}");
                }
            }
            
            // Geben Sie das Projekt mit der höchsten Projektförderung aus
            using (var context = factory.CreateDbContext(args))
            {
                var queryData =
                    from p in context.Projects
                    where
                        (from f in context.Fundings
                            group f by f.ProjectId
                            into projectGroup
                            where projectGroup.Sum(f => f.Amount) ==
                                  (from f in context.Fundings
                                      group f by f.ProjectId
                                      into projectGroup2
                                      select projectGroup2.Sum(f => f.Amount)).Max()
                            select projectGroup.Key).Contains(p.Id)
                    select p;


                foreach (var item in queryData) {
                    Console.WriteLine($"project: {item} ");
                }
            }
            
            // SEW 2
            
            // GEBEN SIE FÜR JEDEN DEBITOR DIE ANZAHL DER GEFÖRDERTEN PROJEKTE AN
            using (var context = factory.CreateDbContext(args))
            {
                var resultSet = from f in context.Fundings
                    group f by f.DebitorId
                    into debitorGroup
                    select new
                    {
                        DebitorId = debitorGroup.Key,
                        ProjectCount = debitorGroup.Count()
                    };

                foreach (var data in resultSet)
                {
                    Console.WriteLine($"debitor: {data.DebitorId} projectCount: {data.ProjectCount}");
                }
            }

            // GEBEN SIE FÜR JEDEN DEBITOR DIE AUFGEWENDETE PROJEKTFÖRDERUNG ZURÜCK
            using (var context = factory.CreateDbContext(args))
            {
                var resultSet = from f in context.Fundings
                    group f by f.DebitorId
                    into debitorGroup
                    select new
                    {
                        DebitorId = debitorGroup.Key,
                        ProjectFunding = debitorGroup.Sum(f => f.Amount)
                    };

                foreach (var data in resultSet)
                {
                    Console.WriteLine($"debitor: {data.DebitorId} funding: {data.ProjectFunding}");
                }
            }
            
            // GEBEN SIE DEN DEBITOR AN, DER DIE HÖCHSTE SUMME FÜR PROJEKTFÖRDERUNGEN AUSGEGEBEN HAT
            // VARIANTE 1
            using (var context = factory.CreateDbContext(args))
            {
                var resultSet =

                    from d in context.Debitors
                    join sub in
                        (
                            from f in context.Fundings
                            group f by f.DebitorId
                            into debitorGroup
                            where debitorGroup.Sum(f => f.Amount) ==

                                  (
                                      from f in context.Fundings
                                      group f by f.DebitorId
                                      into debitorGroup2
                                      select debitorGroup2.Sum(f => f.Amount)
                                  )
                                  .Max()
                            select debitorGroup.Key
                        )
                        on d.Id equals sub
                    select d;

                foreach (var debitor in resultSet)
                {
                    Console.WriteLine(debitor);
                }
            }
            // VARIANTE 2
            using (var context = factory.CreateDbContext(args))
            {
                var resultSet =

                    from d in context.Debitors
                    where
                        (
                            from f in context.Fundings
                            group f by f.DebitorId
                            into debitorGroup
                            where debitorGroup.Sum(f => f.Amount) ==

                                  (
                                      from f in context.Fundings
                                      group f by f.DebitorId
                                      into debitorGroup2
                                      select debitorGroup2.Sum(f => f.Amount)
                                  )
                                  .Max()
                            select debitorGroup.Key
                        )
                        .Contains(d.Id)
                    select d;

                foreach (var debitor in resultSet)
                {
                    Console.WriteLine(debitor);
                }
            }
            
            // FINDEN SIE DIE PROJEKTE MIT DEN MEISTEN SUBPROJEKTEN
            using (var context = factory.CreateDbContext(args))
            {
                var resultSet =

                    from p in context.Projects
                        join sub
                            in
                    
                    (from s in context.Subprojects
                    group s by s.ProjectId
                    into subprojectGroup
                    where subprojectGroup.Count() ==

                          (from s in context.Subprojects
                              group s by s.ProjectId
                              into subprojectGroup2
                              select subprojectGroup2.Count()).Max()
                    select subprojectGroup.Key)
                            
                            on p.Id equals sub
                            select p;
                                

                foreach (var data in resultSet)
                {
                    Console.WriteLine(data);
                }
            }
            
            // GEBEN SIE ALLE REQUESTFUNDINGPROJEKTE AUS DIE MIT MEHR ALS 10000 GEFÖRDERT WERDEN
            using (var context = factory.CreateDbContext(args))
            {
                var resultSet = from p in context.RequestFundingProjects
                    where
                    (
                        from f in context.Fundings
                        group f by f.ProjectId
                        into projectGroup
                        where projectGroup.Sum(f => f.Amount) >= 10000
                        select projectGroup.Key
                    ).Contains(p.Id)
                    select p;

                foreach (var data in resultSet)
                {
                    Console.WriteLine(data);
                }
            }
            
            // GEBEN SIE ALLE SUBPROJEKTE AUS DIE RESEARCHFUNDINGPROKTEN ZUGEORDNET SIND
            using (var context = factory.CreateDbContext(args))
            {
                var resultSet = from p in context.ResearchFundingProjects
                    where
                    (
                        from s in context.Subprojects
                        select s.ProjectId
                    ).Contains(p.Id)
                    select p;

                foreach (var data in resultSet)
                {
                    Console.WriteLine(data);
                }
            }
            
            // GEBEN SIE ALLE DEBITOREN AUS DIE REQUESTFUNDINGPROJEKTE UND RESEARCHFUNDINGPROJEKTE FÖRDERN
            using (var context = factory.CreateDbContext(args))
            {
                

                // HÜ
            }

        }
    }
}