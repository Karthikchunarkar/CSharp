 public class Program { public static void Main (  string[] args ) {
 CreateHostBuilder(args).Build().Run() ;
 }
 public static IHostBuilder CreateHostBuilder (  string[] args ) {
  var builder=Host.CreateDefaultBuilder(args);
 builder.ConfigureWebHostDefaults((  webBuilder ) => {
 webBuilder.UseStartup<Startup>() ;
 }
) ;
 return builder ;
 }
 }