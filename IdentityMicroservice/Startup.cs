using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityMicroservice.DBContexts;
using IdentityMicroservice.Helpers;
using IdentityMicroservice.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace IdentityMicroservice
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
            services.AddCors();
            services.AddControllers();

            services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(Configuration.GetConnectionString("IdentityDB")));

            // appsettings.json i�inde olu�turdu�umuz gizli anahtar�m�z� AppSettings ile �a��raca��m�z� s�yl�yoruz.
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // Olu�turdu�umuz gizli anahtar�m�z� byte dizisi olarak al�yoruz.
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);

            //Projede farkl� authentication tipleri olabilece�i i�in varsay�lan olarak JWT ile kontrol edece�imizin bilgisini kaydediyoruz.
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                //JWT kullanaca��m ve ayarlar� da �unlar olsun dedi�imiz yer ise buras�d�r.
                .AddJwtBearer(x =>
                {
                    //Gelen isteklerin sadece HTTPS yani SSL sertifikas� olanlar� kabul etmesi(varsay�lan true)
                    x.RequireHttpsMetadata = false;
                    //E�er token onaylanm�� ise sunucu taraf�nda kay�t edilir.
                    x.SaveToken = true;
                    //Token i�inde neleri kontrol edece�imizin ayarlar�.
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        //Token 3.k�s�m(imza) kontrol�
                        ValidateIssuerSigningKey = true,
                        //Neyle kontrol etmesi gerektigi
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        //Bu iki ayar ise "aud" ve "iss" claimlerini kontrol edelim mi diye soruyor
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            //DI i�in IUserService aray�z�n� �a��rd���m zaman UserService s�n�f�n� getirmesini s�yl�yorum.
            services.AddScoped<IUserService, UserService>();
        }

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

            //CORS i�in hangi ayarlar� kullanaca��m�z� belirtiyoruz.
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            //Son olarak authentication kullanaca��m�z� belirtiyoruz.
            app.UseAuthentication();
            app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
