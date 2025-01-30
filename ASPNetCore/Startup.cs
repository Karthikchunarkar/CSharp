 public class Startup { readonly private IConfiguration configuration 
 public Startup (  IConfiguration configuration ) {
 this.configuration = configuration ;
 }
 public void ConfigureServices (  IServiceCollection services ) {
 services.AddControllersWithViews() ;
 services.AddSingleton<ASPNetCore.WebSockets.WebSocketManager>() ;
 }
 public void Configure (  IApplicationBuilder app, IWebHostEnvironment env, ILogger < Startup > logger ) {
 if ( env.IsDevelopment() ) {
 app.UseDeveloperExceptionPage() ;
 }
 else {
 app.UseExceptionHandler("/Home/Error") ;
 app.UseHsts() ;
 }
 app.UseHttpsRedirection() ;
 app.UseStaticFiles() ;
 app.UseRouting() ;
 app.UseWebSockets() ;
 app.UseMiddleware<WebSocketManagerMiddleware>() ;
 app.UseEndpoints((  endpoints ) => {
 endpoints.MapControllers() ;
 }
) ;
 }
 }