using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Security;
using Domain.Entity.Admin;
using Domain.Entity.Announcements;
using Domain.Entity.Catalogs;
using Domain.Entity.Other;
using Domain.Entity.Realty;
using Domain.Entity.Shared;
using Reklama.Filters;
using Reklama.Models;
using WebMatrix.WebData;
using Domain.Entity.Articles;

namespace Reklama.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<Reklama.Models.ReklamaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        [InitializeSimpleMembership]
        protected override void Seed(Reklama.Models.ReklamaContext context)
        {
            //SeedMembership(context);
            //SeedMenu(context);
            //SeedCity(context);
            //SeedCurrency(context);

            //SeedCategory(context);
//            SeedSectionsSubsections(context);

            //SeedRealtyCategory(context);
            //SeedRealtySection(context);

            //SeedArticleSection(context);
            //SeedArticleSubsection(context);
            //SeedPage(context);

            //SeedConfig(context);
            //SeedPremium(context);
            //SeedAnnouncementConfiguration(context);
            //SeedArticleConfig(context);

            //SeedRoles();
            //SeedCatalogCategory(context);
            //SeedUnit(context);
            //SeedCatalogParamSection(context);
            //SeedConfig(context);
            //SeedProductParam(context);


            // Seed on 1st production updates
            //SeedCatalogConfig(context);


            // Don't seed this seeds on production server
            //SeedPopularSectionInCatalog(context);
            //SeedNewSectionInCatalog(context);
            //SeedPopularProducts(context);
            //SeedPopularAnnouncement(context);
            //SeedPopularProducts(context);
            //SeedMainPageArticleConfig(context);
        }


        /// �������� ��������������
        private void SeedMembership(Reklama.Models.ReklamaContext context)
        {
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("UserDbConnection", "UserProfile", "UserId", "Email", autoCreateTables: true);
            }

            var roleProvider = (SimpleRoleProvider)Roles.Provider;
            var membershipProvider = (SimpleMembershipProvider)Membership.Provider;

            if (membershipProvider.GetUser("admin@reklama.tm", false) == null)
            {
                membershipProvider.CreateUserAndAccount("admin@reklama.tm", "qwerty2q1");
                var user = context.UserProfiles.First();
                user.Name = "Name";
                user.Surname = "Surname";
                user.Icq = "000000";
                user.Site = "reklama.tm";
                user.Skype = "reklama_skype";
                context.UserProfiles.AddOrUpdate(user);

            }

            if (!roleProvider.RoleExists(ProjectConfiguration.Get.AdministratorRole))
            {
                roleProvider.CreateRole(ProjectConfiguration.Get.AdministratorRole);
            }

            if (!roleProvider.IsUserInRole("admin@reklama.tm", ProjectConfiguration.Get.AdministratorRole))
            {
                roleProvider.AddUsersToRoles(new[] { "admin@reklama.tm" }, new[] { ProjectConfiguration.Get.AdministratorRole });
            }
        }


        private void SeedMenu(Reklama.Models.ReklamaContext context)
        {
            context.Menus.AddOrUpdate(i => i.Id,
                new[]
                    {
                        new Menu()
                            {
                                Id = 1, Name = "UnderLogoMenu", Image = "UnderLogoMenu.png",
                                Description = "��������� ����� ��� ���������"
                            },
                        new Menu()
                            {
                                Id = 2, Name = "AllsDropDownMenu", Image = "AllsDropDownMenu.png",
                                Description = "��������� �� ������� �������� ���� � ���������� ������ &quote;���&quote;"
                            },
                        new Menu()
                            {
                                Id = 3, Name = "FooterMenu", Image = "FooterMenu.png",
                                Description = "��������� � ����� ���� (������) �����"
                            },
                        new Menu()
                        {
                            Id = 4, Name = "FooterBottomMenu", Image = "FooterBottomMenu.png",
                            Description = "��������� ���� ���� ���� ������"
                        }
                    });
        }


        private void SeedCategory(ReklamaContext context)
        {
            context.Categories.AddOrUpdate(
                new Category() { Id = 1, Name = "�������" },
                new Category() { Id = 2, Name = "�������" },
                new Category() { Id = 3, Name = "�����" }
                );
        }


        private void SeedSectionsSubsections(ReklamaContext context)
        {
            context.Sections.AddOrUpdate(
                                        new Section() { Id = 1, Name = "��������" },
                                        new Section() { Id = 2, Name = "����������" },
                                        new Section() { Id = 3, Name = "����" }
                                        );

            context.Subsections.AddOrUpdate(
                new Subsection() { Id = 1, Name = "Nokia", SectionId = 1 },
                new Subsection() { Id = 2, Name = "Motorola", SectionId = 1 },
                new Subsection() { Id = 3, Name = "Apple", SectionId = 2 },
                new Subsection() { Id = 4, Name = "�������������", SectionId = 3 }
                );
        }


        private void SeedConfig(ReklamaContext context)
        {
            //��������
            //������������/�������������� ���� ��� ���������� ��� ����������� ��������(�.16 � �����)
            //context.Configs.AddOrUpdate(
            //    new Config() { Id = 1, Name = "TextInfomationBlock", Description = "��������� �������������� ����" , IsEnable = true , IsEnableValue = true},
            //    new Config() { Id = 2, Name = "CatalogTextPromoBlock", Description = "��������� ����� ����", IsEnable = null, IsEnableValue = true },
            //    new Config() { Id = 5, Name = "NumberActualArticles", Description = "���������� ���������� ������", IsEnable = null, IsEnableValue = true },
            //    new Config() { Id = 6, Name = "NumberPopularArticles", Description = "���������� ���������� ������", IsEnable = null, IsEnableValue = true },
            //    new Config() { Id = 8, Name = "ExpiredAtAnnouncement", Description = "����� �������� ����������", Value = "30", IsEnable = null, IsEnableValue = true },
            //new Config() { Id = 9, Name = "ExpiredAtRealty", Description = "����� �������� ���������� ������������", Value = "30", IsEnable = null, IsEnableValue = true },
            //    new Config() { Id = 10, Name = "UpTimeAnnouncement", Description = "���������� ����� �� �������� ����������", Value = "20", IsEnable = null, IsEnableValue = true },
            //    new Config() { Id = 11, Name = "UpTimeRealty", Description = "���������� ����� �� �������� ���������� ������������", Value = "20", IsEnable = null, IsEnableValue = true },
            //    new Config() { Id = 12, Name = "LinkFacebook", Description = "������ �� ������ � Facebook", Value = "http://facebook.com", IsEnable = null, IsEnableValue = true },
            //    new Config() { Id = 13, Name = "LinkVk", Description = "������ �� ������ � ���������", Value = "http://vk.com", IsEnable = null, IsEnableValue = true },
            //    new Config() { Id = 14, Name = "LinkTwitter", Description = "������ �� ������� Twitter", Value = "http://twitter.com", IsEnable = null, IsEnableValue = true },
            //    new Config() { Id = 15, Name = "LinkGooglePlus", Description = "������ �� ������ � Google plus", Value = "http://plus.google.com", IsEnable = null, IsEnableValue = true },
            //    new Config() { Id = 16, Name = "LinkOdnoklassniki", Description = "������ �� ������ � '�������������'", Value = "http://odnoklassniki.ru", IsEnable = null, IsEnableValue = true },
            //    new Config() { Id = 17, Name = "LinkMail", Description = "������ �� ������ � '��� ���'", Value = "http://mail.ru", IsEnable = null, IsEnableValue = true }
            //    );

            //context.Configs.AddOrUpdate(
            //        new Config() { Id = 18, Name = "UserAgreement", Description = "�������� ����������������� ����������", IsEnable = true, IsEnableValue = true, Value = 1.ToString() },
            //        new Config() { Id = 19, Name = "HowToUpAnnouncement", Description = "��� ������� ����������?", IsEnable = true, IsEnableValue = true, Value = 1.ToString() }

            //new Config() { Id = 20, Name = "IsEnabledPaymentTerminal", Description = "�������� ��������� ��������?", IsEnable = true },
            //new Config() { Id = 21, Name = "Yandex Direct", Description = "�������� ����������� ������� �������?", IsEnable = true, IsEnableValue = true },
            //new Config() { Id = 22, Name = "CountOfPremium1Items", Description = "���������� ��������� �������-1 ����������", IsEnableValue = true, Value = "3" },
            //new Config() { Id = 23, Name = "CountOfPremium2Items", Description = "���������� ��������� �������-2 ����������", IsEnableValue = true, Value = "3" },
            //new Config() { Id = 24, Name = "CountOfPremium3Items", Description = "���������� ��������� �������-3 ����������", IsEnableValue = true, Value = "3" },
            //new Config() { Id = 25, Name = "ROBOPass1", Description = "������ #1 ��� ���������", IsEnableValue = true, Value = "adsaderq534asasd" },
            //new Config() { Id = 26, Name = "ROBOPass2", Description = "������ #2 ��� ���������", IsEnableValue = true, Value = "fhsdkf7#T*Rhjgfhj" },
            //new Config() { Id = 27, Name = "ROBOLogin", Description = "����� ��� ���������", IsEnableValue = true, Value = "maximahmedov" }
            //);
            context.Configs.AddOrUpdate(new Config() { Id = 28, Name = "ChangeRegistrationDataHelp", Description = "��������� ��������� ��� ��������� ��������������� ������ ��������", IsEnableValue = true, Value = "" });
        }


        private void SeedCity(ReklamaContext context)
        {
            //Cities
            context.Cities.AddOrUpdate(
                new City() { Id = 1, Name = "�������" },
                new City() { Id = 2, Name = "������" },
                new City() { Id = 3, Name = "�����" },
                new City() { Id = 4, Name = "��������" },
                new City() { Id = 5, Name = "���������" },
                new City() { Id = 6, Name = "����������" },
                new City() { Id = 7, Name = "�������" },
                new City() { Id = 8, Name = "��������" },
                new City() { Id = 9, Name = "��������" },
                new City() { Id = 10, Name = "ø�����" },
                new City() { Id = 11, Name = "�������" },
                new City() { Id = 12, Name = "�����" },
                new City() { Id = 13, Name = "���������" },
                new City() { Id = 14, Name = "ʸ��������" },
                new City() { Id = 15, Name = "������" },
                new City() { Id = 16, Name = "��������" },
                new City() { Id = 17, Name = "����" },
                new City() { Id = 18, Name = "�����" },
                new City() { Id = 19, Name = "������" },
                new City() { Id = 20, Name = "����������" },
                new City() { Id = 21, Name = "������" },
                new City() { Id = 22, Name = "�����������" },
                new City() { Id = 23, Name = "�����������" },
                new City() { Id = 24, Name = "�����" },
                new City() { Id = 25, Name = "������" },
                new City() { Id = 26, Name = "--������" }
                );
        }





        private void SeedCurrency(ReklamaContext context)
        {
            //Currency
            context.Currencies.AddOrUpdate(
                new Currency() { Id = 1, Name = "USD", Rate = 1 },
                //new Currency() { Id = 2, Name = "EUR", Rate = 1.41f },
                new Currency() { Id = 3, Name = "TMT", Rate = 2.85f }
                );
        }


        private void SeedPage(ReklamaContext context)
        {
            context.Pages.AddOrUpdate(
                new Page() { Id = 1, Name = "��������", Description = "���� ��������", CreatedAt = DateTime.Now, IsActive = true, PageTemplateId = 1, Slug = "our-contacts" }
            );
        }


        private void SeedRealtyCategory(ReklamaContext context)
        {
            //RealtyCategory
            context.RealtyCategories.AddOrUpdate(
                new RealtyCategory() { Id = 1, Name = "���" },
                new RealtyCategory() { Id = 2, Name = "��������" },
                new RealtyCategory() { Id = 3, Name = "�������" }
                );
        }


        private void SeedRealtySection(ReklamaContext context)
        {
            //RealtySection
            context.RealtySections.AddOrUpdate(
                new RealtySection() { Id = 1, Name = "������" },
                new RealtySection() { Id = 2, Name = "�����" },
                new RealtySection() { Id = 3, Name = "�������" },
                new RealtySection() { Id = 4, Name = "����" },
                new RealtySection() { Id = 5, Name = "�����" },
                new RealtySection() { Id = 6, Name = "������" }
                );
        }


        private void SeedArticleSection(ReklamaContext context)
        {
            context.ArticleSections.AddOrUpdate(
                new ArticleSection() { Id = 1, Name = "��������" },
                new ArticleSection() { Id = 2, Name = "����������" },
                new ArticleSection() { Id = 3, Name = "����������" }
            );
        }

        private void SeedArticleSubsection(ReklamaContext context)
        {
            context.ArticleSubsections.AddOrUpdate(
                new ArticleSubsection() { Id = 1, Name = "Mototola", SectionId = 1 },
                new ArticleSubsection() { Id = 2, Name = "Apple", SectionId = 2 },
                new ArticleSubsection() { Id = 3, Name = "����", SectionId = 3 },
                new ArticleSubsection() { Id = 4, Name = "Author", SectionId = 3 }
            );
        }


        private void SeedPremium(ReklamaContext context)
        {
            context.Premiums.AddOrUpdate(
                new Premium() { Id = 1, Name = "������1 �����", Description = "������� ������� ������ ��� ����� ����������", Cost = 15, Lifetime = 72 },
                new Premium() { Id = 2, Name = "������2 �����", Description = "������� ������� ������ ��� ����� ����������", Cost = 10, Lifetime = 72 },
                new Premium() { Id = 3, Name = "������3 �����", Description = "������� �������� ������ ��� ����� ����������", Cost = 5, Lifetime = 72 },

                new Premium() { Id = 4, Name = "������1 ������������", Description = "������� ������� ������ ��� ������������", Cost = 15, Lifetime = 72 },
                new Premium() { Id = 5, Name = "������2 ������������", Description = "������� ������� ������ ��� ������������", Cost = 10, Lifetime = 72 },
                new Premium() { Id = 6, Name = "������3 ������������", Description = "������� �������� ������ ��� ������������", Cost = 5, Lifetime = 72 }
                );
        }


        private void SeedAnnouncementConfiguration(ReklamaContext context)
        {
            context.AnnouncementConfigs.AddOrUpdate(
                new AnnouncementConfig() { Id = 1, Slogan = "����������. ��� ���������� ������ ������ �����", Premium1Id = 1, Premium2Id = 2, Premium3Id = 3 }
                );
        }


        private void SeedArticleConfig(ReklamaContext context)
        {
            context.ArticleConfigs.AddOrUpdate(
                new ArticleConfig() { Id = 1, Slogan = "������. ��� ���������� ������ ������ �����" }
                );
        }


        private void SeedRoles()
        {
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("UserDbConnection", "UserProfile", "UserId", "Email", autoCreateTables: true);
            }

            var roleProvider = (SimpleRoleProvider)Roles.Provider;
            //roleProvider.CreateRole("Moderator");
            roleProvider.CreateRole("Shop");
        }


        private void SeedCatalogCategory(ReklamaContext context)
        {
            context.CatalogCategories.AddOrUpdate(
                new CatalogCategory() { Id = 1, Name = "��������" },
                new CatalogCategory() { Id = 2, Name = "����������" }
            );

            context.CatalogSecondCategories.AddOrUpdate(
                new CatalogSecondCategory() { Id = 1, CategoryId = 1, Name = "��������� ��������" },
                new CatalogSecondCategory() { Id = 2, CategoryId = 1, Name = "��������� ��������" },
                new CatalogSecondCategory() { Id = 3, CategoryId = 2, Name = "������������ ����������" },
                new CatalogSecondCategory() { Id = 4, CategoryId = 2, Name = "��������" }
            );

            context.CatalogThirdCategories.AddOrUpdate(
                new CatalogThirdCategory() { Id = 1, SecondCategoryId = 1, Name = "Redtel" },
                new CatalogThirdCategory() { Id = 2, SecondCategoryId = 1, Name = "GreenTel" },
                new CatalogThirdCategory() { Id = 3, SecondCategoryId = 2, Name = "Motorola" },
                new CatalogThirdCategory() { Id = 4, SecondCategoryId = 2, Name = "Nokia" },
                new CatalogThirdCategory() { Id = 5, SecondCategoryId = 3, Name = "PhiniX" },
                new CatalogThirdCategory() { Id = 6, SecondCategoryId = 3, Name = "iMac" },
                new CatalogThirdCategory() { Id = 7, SecondCategoryId = 4, Name = "Apple" },
                new CatalogThirdCategory() { Id = 8, SecondCategoryId = 4, Name = "Lenovo" }
            );
        }


        private void SeedUnit(ReklamaContext context)
        {
            context.Units.AddOrUpdate(
                new Unit() { Id = 1, Name = "" },
                new Unit() { Id = 2, Name = "��" },
                new Unit() { Id = 3, Name = "�����" },
                new Unit() { Id = 4, Name = "��" }
            );
        }


        private void SeedCatalogParamSection(ReklamaContext context)
        {
            context.CatalogParamSubsections.AddOrUpdate(
                new CatalogParamSubsection() { Id = 1, SecondCategoryId = 1, Name = "����� �������", Slogan = "����� ������� ���������� ��������" },
                new CatalogParamSubsection() { Id = 2, SecondCategoryId = 1, Name = "�������������� �������", Slogan = "�������������� ������� ���������� ��������" },
                new CatalogParamSubsection() { Id = 3, SecondCategoryId = 2, Name = "�������������� �������", Slogan = "�������������� ������� ���������� ��������" },
                new CatalogParamSubsection() { Id = 4, SecondCategoryId = 2, Name = "�������������� ����������", Slogan = "�������������� ���������� ���������� ��������" },
                new CatalogParamSubsection() { Id = 5, SecondCategoryId = 3, Name = "�������������� ����������", Slogan = "�������������� ���������� ��" },
                new CatalogParamSubsection() { Id = 6, SecondCategoryId = 4, Name = "�������������� ����������", Slogan = "�������������� ���������� ��������" },
                new CatalogParamSubsection() { Id = 7, SecondCategoryId = 4, Name = "��������", Slogan = "�������� ��������" }
            );
        }


        private void SeedProductParam(ReklamaContext context)
        {
            context.ProductParams.AddOrUpdate(
                new ProductParam() { Id = 1, Name = "��� �������" },
                new ProductParam() { Id = 2, Name = "���" },
                new ProductParam() { Id = 3, Name = "��������" },
                new ProductParam() { Id = 4, Name = "����� ������" },
                new ProductParam() { Id = 5, Name = "��������" }
            );
        }

        private void SeedCatalogConfig(ReklamaContext context)
        {
            context.CatalogConfigs.AddOrUpdate(
                new CatalogConfig()
                {
                    Id = 1,
                    Slogan = "�������. ��� ����� ����� ��������� �� ����� ������������",
                    PromoText = "<strong class=\"orangeStrong\">��������� �������</strong><br/>���-���",
                    RegShopPromoText = "<p>��������� ������� ���-������ ��� �������� ������� �������� ����� ������.</p>\n<span>���� ������������</span>\n<ul>\n<li><img src=\"/Images/System/regN1.png\" />������� ����� �������� ���� ��� ����� ������������ ������� ����� ������������</li>\n<li><img src=\"/Images/System/regN2.png\" />������� ����� �������� ���� ��� ����� ������������ ������� ����� ������������</li>\n<li><img src=\"/Images/System/regN3.png\" />������� ����� �������� ���� ��� ����� ������������ ������� ����� ������������</li>\n<li><img src=\"/Images/System/regN4.png\" />������� ����� �������� ���� ��� ����� ������������ ������� ����� ������������</li>\n</ul>",
                    WarningText = "����������� ����������� ��������� �������� ��������� ����������� � �� ������������ ������ �����. ��� ������� ������ ����������� ������ ������������ ������������ ���� ��� �� ��������. ����������� ��������� �������� ��������, ���� ������ � ���� ���������� � �������� ������."
                }
                );
        }


        private void SeedPopularSectionInCatalog(ReklamaContext context)
        {
            context.PopularSectionsInCatalog.AddOrUpdate(
                new PopularSectionInCatalog() { Id = 1, SectionId = 1 },
                new PopularSectionInCatalog() { Id = 2, SectionId = 2 },
                new PopularSectionInCatalog() { Id = 3, SectionId = 3 },
                new PopularSectionInCatalog() { Id = 4, SectionId = 4 },
                new PopularSectionInCatalog() { Id = 5, SectionId = 5 }
                );
        }

        private void SeedNewSectionInCatalog(ReklamaContext context)
        {
            context.NewSectionsInCatalog.AddOrUpdate(
                new NewSectionInCatalog() { Id = 1, SectionId = 1 },
                new NewSectionInCatalog() { Id = 2, SectionId = 2 },
                new NewSectionInCatalog() { Id = 3, SectionId = 3 },
                new NewSectionInCatalog() { Id = 4, SectionId = 4 }
                );
        }

        private void SeedPopularProducts(ReklamaContext context)
        {
            context.PopularProducts.AddOrUpdate(
                new PopularProduct() { Id = 1, ProductId = 1 },
                new PopularProduct() { Id = 2, ProductId = 19 },
                new PopularProduct() { Id = 3, ProductId = 20 },
                new PopularProduct() { Id = 4, ProductId = 21 },
                new PopularProduct() { Id = 5, ProductId = 24 }
                );
        }

        private void SeedPopularAnnouncement(ReklamaContext context)
        {
            context.PopularAnnouncement.AddOrUpdate(
                new PopularAnnouncement() { Id = 1, AnnouncementId = 19 },
                new PopularAnnouncement() { Id = 2, AnnouncementId = 20 },
                new PopularAnnouncement() { Id = 3, AnnouncementId = 21 },
                new PopularAnnouncement() { Id = 4, AnnouncementId = 11 },
                new PopularAnnouncement() { Id = 5, AnnouncementId = 12 },
                new PopularAnnouncement() { Id = 6, AnnouncementId = 5 });


        }
        private void SeedMainPageArticleConfig(ReklamaContext context)
        {
            context.MainPageArticleConfigs.AddOrUpdate(
                new MainPageArticleConfig()
                    {
                        Article1Id = 26,
                        Article2Id = 25,
                        Article3Id = 24,
                        Article4Id = 22
                    }
                );
        }
    }
}
