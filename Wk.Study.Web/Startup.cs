using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Autofac.Features.Indexed;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Wk.Study.IService;
using Wk.Study.Library.ConfigModel;
using Wk.Study.Model.Models;
using Wk.Study.Service.ProfileMapping;
using Wk.Study.Service.Services;
using Wk.Study.Web.filter;
using Wk.Study.Web.Utils;

namespace Wk.Study.Web
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
            services.AddHttpClient();

            var mapperConfig = new MapperConfiguration(e => e.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            var jwtSetting = new JwtSetting();
            Configuration.Bind("JwtSetting", jwtSetting);
            services.AddAuthentication(options=> {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options=> {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.SecurityKey)),
                        ValidIssuer = jwtSetting.Issuer,
                        ValidAudience = jwtSetting.Audience,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.Configure<JwtSetting>(Configuration.GetSection("JwtSetting"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            services.AddDbContext<wkstudyContext>(options =>
                      options.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection")));
            services.AddControllers(t=> {
                //t.Filters.Add(new WkExceptionFilter());
                t.Filters.Add(new WkActionFilter());

            }).AddControllersAsServices();
            services.AddControllers();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //һ���ӿڶ��ʵ��
            //1����Ҫָ����ֵ ��һ��Object����
            // 2��ע�����ʹ�÷���Keyed  ����Ϊָ���ļ�ֵ�е�ֵ ��ÿһ�������ʵ�ֺͼ�ֵҪһһ��Ӧ���������ﲻ���ظ���
            // 3����ȡ���� ֱ��ͨ��ResolveKeyed() ��ȡ�����ޣ�������Ҫ���� ָ����Ӧ�ļ�ֵ
            //       �Ȼ�ȡһ��IIndex����ͨ��IInex ��������ȡ�����ʵ��
            //            builder.RegisterType<TestServiceD>().Keyed<ITestServiceD>(DeviceState.TestServiceD);
            //            builder.RegisterType<TestServiceD_One>().Keyed<ITestServiceD>(DeviceState.TestServiceD_One);
            //            builder.RegisterType<TestServiceD_Two>().Keyed<ITestServiceD>(DeviceState.TestServiceD_Two);
            //            builder.RegisterType<TestServiceD_Three>().Keyed<ITestServiceD>(DeviceState.TestServiceD_Three);

            // Ϊ��ͬ��ʵ��ָ�����ƣ�����Ƚϼ򵥣��Ƽ�

            //containerBuilder.RegisterType<TestServiceD_Three>().Named<ITestServiceD>("three");


            // IIndex<DeviceState, ITestServiceD> index = container.Resolve<IIndex<DeviceState, ITestServiceD>>();

            //ITestServiceD testServiceD = index[DeviceState.TestServiceD];
            //ITestServiceD TestServiceD_One = index[DeviceState.TestServiceD_One];
            //ITestServiceD TestServiceD_Two = index[DeviceState.TestServiceD_Two];
            //ITestServiceD TestServiceD_Three = index[DeviceState.TestServiceD_Three];

            // �������ƽ���
            //var t2 = container.ResolveNamed<ITestServiceD>("three");

            //IContainer container = containerBuilder.Build();
            builder.RegisterType<JwtService>();
            builder.RegisterType<WkInterceptor>(); // Ҫ��ע��������
            builder.RegisterType<UserService>().As<IUserService>().SingleInstance();
            var controllerBaseType = typeof(ControllerBase);
            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType)
                .PropertiesAutowired()
                .EnableClassInterceptors(); // ������Controller����ʹ��������
                                            //[Intercept(typeof(TestInterceptor))] ����Ҫʹ�������������ӿ����������

       
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}
            app.UseHttpsRedirection();
            //app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();
    
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
