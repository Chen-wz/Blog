using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Blog.Core.Test5._0
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
            services.AddMemoryCache();
            services.AddMvc();
            #region ���xml��ʽ
            //services.AddMvc(options =>
            //{
            //    options.ReturnHttpNotAcceptable = true;
            //    options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            //});
            #endregion
            //services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Blog.Core.Test5._0", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region Environment
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog.Core.Test5._0 v1"));
            }
            else
            {
                app.UseExceptionHandler("/Erroe");
            }
            #endregion
            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //typeof(ApiVersions).GetEnumNames().OrderByDescending(e => e).ToList().ForEach(vsersion =>
                //  {
                //      c.SwaggerEndpoint($"swagger/{vsersion}/swagger.json",$"{ApiName}{version}");
                //  });
                c.IndexStream = () => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("Blog.Core.Index.html");
                c.RoutePrefix = "";
                //c.IndexStream = () => Assembly.GetExecutingAssembly().GetManifestResourceStream();
            });
            #endregion
            #region MiniProfiler
            //app.UseMiniProfiler();
            #endregion
            #region CORS
            //����ڶ��ַ�����ʹ�ò��ԣ���ϸ������Ϣ��ConfigureService��
            app.UseCors("LimitRequests");
            #region �����һ�ְ汾
            //app.UseCors(options => options.WithOrigins("http://localhost:5000").AllowAnyHeader().AllowAnyMethod());
            #endregion
            #endregion
            //��תhttps
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseStatusCodePages();//��״̬�뷵�ظ�ǰ̨

            app.UseRouting();
            #region ��������������֤�м��
            app.UseAuthentication();
            #endregion
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
