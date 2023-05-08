using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class first_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Albums_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AlbumImage",
                columns: table => new
                {
                    AlbumId = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumImage", x => new { x.AlbumId, x.ImageId });
                    table.ForeignKey(
                        name: "FK_AlbumImage_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlbumImage_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Posts_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Posts_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reactions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reactions_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "Guid", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "ef1773ed-f5fc-4d58-adcf-80565d904308", null, true, new Guid("678daebc-b951-4dcd-999a-a79179ca4c26"), false, null, null, null, null, null, true, null, false, "Francis" },
                    { 2, 0, "e0b9485e-58ac-4000-9566-7d0d7f42dfdd", null, true, new Guid("08879b62-aae6-466f-90e1-ace9013706fb"), false, null, null, null, null, null, true, null, false, "Cassidy" },
                    { 3, 0, "cb8b92f7-1fc9-4811-8ae8-ae478149f088", null, true, new Guid("5bf3907b-e5e8-412b-aede-10a41bb924b2"), false, null, null, null, null, null, true, null, false, "Bell" },
                    { 4, 0, "75e5a0c3-d66c-443f-808f-142bb3535793", null, true, new Guid("85e13ed7-e132-4d1f-b503-eed5df97be5b"), false, null, null, null, null, null, true, null, false, "Cordell" },
                    { 5, 0, "7a7f1e9e-6572-4061-82bf-c73a8595309b", null, true, new Guid("ee05649a-3ae6-498c-a5bf-4336458d6fd4"), false, null, null, null, null, null, true, null, false, "Jarret" }
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Blocked" },
                    { 2, "Blocked" },
                    { 3, "Hidden" }
                });

            migrationBuilder.InsertData(
                table: "Albums",
                columns: new[] { "Id", "Description", "Guid", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, "Consectetur unde earum.\nAutem quia molestiae rem deserunt nemo quia similique dicta sint.\nNihil nulla et.", new Guid("ba7e1e0c-6b84-4df9-b669-2603da66bc11"), "aperiam aut omnis rerum", 1 },
                    { 2, "Neque sint molestias.\nMolestiae illum vel est itaque.\nAliquid numquam sint debitis adipisci fugiat.", new Guid("5c1913fb-8ee3-4d95-88fa-47db4adcbc0b"), "inventore repudiandae possimus quam", 1 },
                    { 3, "Doloribus nulla perspiciatis dolor rerum totam neque.\nDolores officia sunt officia ratione dolorem nemo quia qui iure.\nQui nemo quia quis eligendi.", new Guid("431f3838-32df-4c7b-9cd1-52c4d93df58e"), "non eligendi explicabo nihil", 1 },
                    { 4, "Facilis nihil aut aspernatur blanditiis.\nEt dicta vitae qui rem et voluptas ratione sint sunt.\nConsequatur voluptas non aut et sint ipsam nihil.", new Guid("ab9739ac-10bc-4648-b508-b078210ee74d"), "odit optio molestias nostrum", 1 },
                    { 5, "Iure fuga tempora.\nAd voluptates asperiores quisquam dicta placeat minima.\nDolorem assumenda rerum incidunt esse iste nemo officia.", new Guid("da6a2b32-b6b3-4ece-8768-522ff4ea5b5c"), "qui harum velit tempora", 1 }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "Guid", "Slug", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, new Guid("de7f6644-25f8-4901-907f-a5418af22375"), "", "in qui quod", 1 },
                    { 2, new Guid("2982a05e-df99-431b-ae57-f28bbe076fc8"), "", "ipsam corporis non", 1 },
                    { 3, new Guid("0bf3cd1c-6d30-43e1-8d7c-55077db3c5a2"), "", "ad nam non", 1 },
                    { 4, new Guid("2fc8e0c4-a6a2-43ea-9c5c-f0f0cff1685d"), "", "quis porro qui", 1 },
                    { 5, new Guid("3158e776-7bdd-4a25-ad80-b79ec07b100b"), "", "distinctio dolorem sunt", 1 },
                    { 6, new Guid("a53b661b-c127-4fae-aff2-a6f1c1f0b459"), "", "tempora in enim", 1 },
                    { 7, new Guid("6140dc31-4132-46ca-b2bc-f565c7fd4e31"), "", "aut consequatur quam", 1 },
                    { 8, new Guid("825b304f-9c08-48c5-ad33-d3c3f3beb44f"), "", "ut qui inventore", 1 },
                    { 9, new Guid("873c0305-4d0c-483e-a062-d53f3e5dc665"), "", "doloribus libero facilis", 1 },
                    { 10, new Guid("270d48ff-a98d-430b-ba4b-48841af5e650"), "", "est consequatur sit", 1 },
                    { 11, new Guid("e101ab05-a7f0-4b2b-a166-0d6e0e031723"), "", "voluptas mollitia quia", 1 },
                    { 12, new Guid("33b7616c-bb6a-4c30-82bf-8309847a1748"), "", "velit voluptas nemo", 1 },
                    { 13, new Guid("8f3f3da1-7d2e-4697-ab9e-9b53cdeeb55b"), "", "eum eos omnis", 1 },
                    { 14, new Guid("cc4c74ca-6f5e-4d77-b9f6-8342c2d99b4d"), "", "beatae perspiciatis ea", 1 },
                    { 15, new Guid("8f76fa4b-09fa-47c6-bb91-f19c1c6acb2e"), "", "occaecati aliquam earum", 1 },
                    { 16, new Guid("cf971e6c-fffb-44b4-bd53-e2a174ef6e89"), "", "culpa aut alias", 1 },
                    { 17, new Guid("8bed8163-6d5a-4a04-8bf5-ca410c538519"), "", "tempora suscipit non", 1 },
                    { 18, new Guid("5e7fae14-34aa-40af-9973-50aa17702bac"), "", "ratione sapiente inventore", 1 },
                    { 19, new Guid("0026b873-ab0a-4384-81c6-65d7a9f26b34"), "", "iusto aut voluptatem", 1 },
                    { 20, new Guid("fa987ffe-d5db-4a64-a25c-83a570218796"), "", "doloribus maxime occaecati", 1 }
                });

            migrationBuilder.InsertData(
                table: "AlbumImage",
                columns: new[] { "AlbumId", "ImageId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 1, 5 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 2, 4 },
                    { 2, 5 },
                    { 3, 1 },
                    { 3, 2 },
                    { 3, 3 },
                    { 3, 4 },
                    { 3, 5 },
                    { 3, 6 },
                    { 3, 7 },
                    { 3, 8 },
                    { 3, 9 },
                    { 3, 10 },
                    { 3, 11 },
                    { 3, 12 },
                    { 3, 13 },
                    { 3, 14 },
                    { 3, 15 },
                    { 4, 1 },
                    { 4, 2 },
                    { 4, 3 },
                    { 4, 4 },
                    { 4, 5 },
                    { 4, 6 },
                    { 4, 7 },
                    { 4, 8 },
                    { 4, 9 },
                    { 5, 1 },
                    { 5, 2 },
                    { 5, 3 },
                    { 5, 4 }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Guid", "ImageId", "StatusId", "Tags", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, new Guid("f4dca02c-b6b7-439b-9438-d2900884085e"), 1, 2, "Plastic|methodical|Jordan", "fugit aut expedita mollitia", 1 },
                    { 2, new Guid("0e3ef112-5031-41c1-886a-50f0f2bd9651"), 2, 2, "payment|attitude-oriented|Refined Cotton Chair", "aperiam nam nihil dignissimos", 1 },
                    { 3, new Guid("98522764-dec5-46f4-9922-fc50f7e12646"), 3, 2, "dot-com|lavender|customized", "cupiditate et et aspernatur", 1 },
                    { 4, new Guid("c835339b-15f3-4a42-a400-7c948f2945f5"), 4, 3, "New Mexico|redundant|Frozen", "repellendus voluptatem consequuntur quidem", 1 },
                    { 5, new Guid("4b2afd9f-0385-4c59-9f96-4054ddcc39e8"), 5, 1, "Bangladesh|IB|networks", "hic dolor saepe modi", 1 },
                    { 6, new Guid("1d98c895-4caa-4d25-9c46-7a2482e0d77a"), 6, 2, "Gorgeous Metal Table|encompassing|embrace", "vel qui pariatur ea", 1 },
                    { 7, new Guid("2cae47fc-733a-47eb-98f5-8ce5bb7d0128"), 7, 3, "Tanzanian Shilling|Money Market Account|maroon", "voluptate recusandae quaerat nobis", 1 },
                    { 8, new Guid("a57340a0-1f88-40fd-a258-0c6b4b0f90a9"), 8, 2, "Borders|methodology|intermediate", "assumenda tenetur unde est", 1 },
                    { 9, new Guid("a0160070-2c85-489f-9944-9d0bb841e955"), 9, 1, "Home & Automotive|granular|stable", "quis aut minus incidunt", 1 },
                    { 10, new Guid("12638026-4874-4964-9da1-9451b6baf451"), 10, 1, "Assurance|holistic|New Jersey", "unde libero est sit", 1 },
                    { 11, new Guid("cc9378c3-29ad-4a04-87f4-d26691783d2c"), 11, 3, "backing up|scalable|Intelligent Concrete Soap", "magni dolores expedita reprehenderit", 1 },
                    { 12, new Guid("08b55f96-417a-4b24-9018-0e03d1a9f202"), 12, 1, "product|synthesizing|Architect", "labore exercitationem molestias distinctio", 1 },
                    { 13, new Guid("ea0e2f5b-e5b4-4825-8552-d023a24e590c"), 13, 2, "Fantastic Steel Chicken|overriding|Profound", "qui eos earum nihil", 1 },
                    { 14, new Guid("4bf87829-070f-457d-8aaf-3926bcd8a633"), 14, 2, "Vision-oriented|transmit|Refined Rubber Fish", "earum harum voluptas sit", 1 },
                    { 15, new Guid("263408a6-8f1c-4d62-bf94-daffa8612e74"), 15, 3, "tan|Direct|distributed", "eos soluta voluptatem cumque", 1 },
                    { 16, new Guid("180fd4e3-e9e1-49f3-b7ac-523596561e57"), 16, 1, "Producer|impactful|Decentralized", "quas nisi accusamus eos", 1 },
                    { 17, new Guid("8ed89c05-40a4-48bc-a1bf-769a18c8ce2b"), 17, 2, "invoice|Avon|incubate", "libero ab omnis soluta", 1 },
                    { 18, new Guid("bf26a306-a7c9-42d5-be2a-971e6da276e2"), 18, 3, "models|AI|Producer", "pariatur reprehenderit inventore nostrum", 1 },
                    { 19, new Guid("16d8beeb-5f8d-4aff-9100-17c04fcdc18c"), 19, 3, "Kansas|Persevering|intermediate", "sed error corporis qui", 1 },
                    { 20, new Guid("58ed7f2b-fda0-4e79-9c11-b14aa9546683"), 20, 1, "Seychelles Rupee|24/365|transform", "ut delectus quia deleniti", 1 }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Guid", "PostId", "Text", "UserId" },
                values: new object[,]
                {
                    { 1, new Guid("093819ad-c804-4a70-b977-1e468b0a0ae1"), 1, "Suscipit est cum in perspiciatis odio voluptatem aut.\nEt quaerat quo molestias.", 5 },
                    { 2, new Guid("e382f437-b82e-40ef-b4a4-a2436b9b275e"), 1, "Ullam a fugiat voluptate quis necessitatibus.\nSequi et veritatis nesciunt necessitatibus nulla odit.", 4 },
                    { 3, new Guid("00a310d1-8d7e-41d4-9923-c8e39e3b56d8"), 1, "Temporibus velit rerum quo dolor est deserunt.\nSit cupiditate provident tenetur.", 2 },
                    { 4, new Guid("59a1102a-6680-4b4c-8622-3e1469dbb098"), 1, "Qui quam provident aliquam consequatur ut omnis.\nOmnis error animi ex inventore sunt.", 2 },
                    { 5, new Guid("dfc3a9a1-e090-405b-8032-005b4fcc0972"), 1, "Quisquam modi quas consequatur provident facere ipsa et provident.\nAt omnis occaecati odit.", 4 },
                    { 6, new Guid("22b80906-b51a-47dd-bc1a-806823f72806"), 1, "Vero commodi aspernatur dignissimos aliquid sit rerum.\nAutem harum est praesentium.", 5 },
                    { 7, new Guid("64025ebf-806b-4541-af23-167636f30e0d"), 1, "Laudantium molestiae dolores voluptatem ipsam dicta aut.\nVoluptatem error rem quisquam sequi esse ipsam.", 4 },
                    { 8, new Guid("a2734ec9-2ee6-4961-b58d-066c988c8f4c"), 1, "Voluptates enim error aut aliquid.\nModi suscipit illum aut totam praesentium quo et commodi quia.", 1 },
                    { 9, new Guid("20ecf86b-577d-4e8f-b511-f560f76ce573"), 1, "Dignissimos minus nemo ut omnis similique in.\nAut quisquam reprehenderit veritatis est aliquid iste ad officiis.", 3 },
                    { 10, new Guid("05bab625-1ecf-4f31-b175-3a869f8ae4e1"), 1, "Perspiciatis voluptas enim nesciunt deleniti a.\nQuos asperiores ut voluptatum quibusdam repudiandae.", 2 },
                    { 11, new Guid("30e5b60a-7707-4b29-9cbf-effd18050afb"), 2, "Autem voluptatibus autem optio sed nostrum voluptatem dolor dolores.\nEt consequatur consequatur aut porro exercitationem debitis porro aut.", 2 },
                    { 12, new Guid("589d293c-0878-455f-8053-bf4a1ae49a01"), 2, "Minus modi reprehenderit officia repellendus inventore.\nDolor quo et modi et qui fuga quia id.", 2 },
                    { 13, new Guid("e7e28a33-c22a-4031-ba0e-3babbab98143"), 2, "Non ratione sit voluptas esse facilis.\nQuidem quam dolore impedit quod accusamus.", 5 },
                    { 14, new Guid("09abc7a6-53c6-4dd6-bf96-fac050bb6ac6"), 2, "Ut non est.\nCulpa ratione maxime excepturi.", 3 },
                    { 15, new Guid("0cd0691d-fa14-45df-8982-2619d0463837"), 2, "Et eum vel rerum in.\nVelit et et eos quia et.", 5 },
                    { 16, new Guid("fd2c61cc-8e38-4c9f-b2f4-c9d0b6f22a8e"), 2, "Reiciendis consequuntur nesciunt laudantium.\nSint est earum et dicta quod itaque ut consequatur nesciunt.", 3 },
                    { 17, new Guid("735168c5-21a8-41fe-93ce-0bb6256e8982"), 2, "Unde aut ut quia totam quia reiciendis.\nUllam eos id voluptas at mollitia veniam consequatur in nostrum.", 2 },
                    { 18, new Guid("1503ad3b-74a7-4a93-875a-3fee9f8c6fd3"), 2, "Mollitia est nihil labore corporis eligendi cupiditate blanditiis voluptatem sit.\nQuis voluptas soluta aut voluptatem incidunt quidem molestiae omnis qui.", 4 },
                    { 19, new Guid("b25933fc-8d38-4993-a110-28884f1d55fc"), 2, "Qui qui iusto molestiae est quo necessitatibus unde ex.\nEt voluptatum libero necessitatibus vitae sunt explicabo ea.", 3 },
                    { 20, new Guid("f8419dfc-70f0-4b4b-9f3f-81b9fdc40d5d"), 2, "Odit animi ea omnis sit ratione et.\nEligendi omnis ut qui porro ratione dolor.", 4 },
                    { 21, new Guid("f00a7a2a-cb69-42ef-8e82-b15820fc6829"), 3, "Asperiores eveniet omnis.\nEos iste rerum ipsam sit iure ut voluptates.", 5 },
                    { 22, new Guid("7876aff4-de55-49fe-a5d0-b7898b915b81"), 3, "Quae rem incidunt asperiores deserunt vero consequatur aperiam quia autem.\nId quidem facilis.", 5 },
                    { 23, new Guid("293f8b06-84ad-4a9d-b179-916ade08abdc"), 3, "Voluptatum perferendis commodi et similique consequatur nulla optio quisquam.\nQuia architecto ut praesentium quo tenetur pariatur provident omnis.", 1 },
                    { 24, new Guid("9139488f-5434-495b-8d04-4790e23f06b5"), 3, "Sequi magni porro iste eum quasi et dicta.\nVoluptas optio nemo qui facere possimus adipisci.", 3 },
                    { 25, new Guid("56700d3d-237e-48eb-b018-214919fb8a84"), 3, "Voluptas minima sit.\nVel perspiciatis est earum cum cupiditate sit dolorem.", 3 },
                    { 26, new Guid("bf90f019-ff1d-4a76-a969-33553e2488b9"), 3, "Neque quia vitae nisi fuga.\nEnim ut quis cumque accusamus eaque sed veniam aspernatur.", 3 },
                    { 27, new Guid("766af6cb-3647-477b-85ac-a43dcd97e77c"), 3, "Eius non repellat id ea esse deleniti nulla.\nEt dolores voluptatibus neque impedit.", 5 },
                    { 28, new Guid("23c0c8bf-d67a-4c34-aea6-3e0fdec9e9c3"), 3, "Iste est vel debitis et.\nUt ipsam dicta ratione eum perspiciatis voluptate illo sed et.", 5 },
                    { 29, new Guid("335a6e1a-6050-4563-8cb3-e518328bba4e"), 3, "Beatae nihil molestias eligendi consequatur qui et deserunt sapiente sint.\nId sunt earum porro.", 4 },
                    { 30, new Guid("32fcc79c-7d38-42b8-969e-8c2d2bd4daa4"), 3, "Veritatis labore et voluptatem deleniti accusamus fugiat quasi.\nImpedit eos natus iure veritatis sapiente officiis omnis recusandae aut.", 3 },
                    { 31, new Guid("42020366-d119-44fc-87a6-5f0f88cb43d4"), 4, "Aut optio rerum temporibus est id.\nId qui omnis non voluptas reiciendis ut libero voluptate.", 1 },
                    { 32, new Guid("12379891-c12c-40ce-915f-f3a55516d093"), 4, "At quas totam saepe vel.\nVel eum dolores explicabo quidem illo deleniti odit expedita.", 2 },
                    { 33, new Guid("88067264-21c1-4acb-85c3-1fe22166164b"), 4, "Aut saepe aut nemo et quia tempore.\nConsequatur dicta fugit autem magnam cupiditate dolorem.", 3 },
                    { 34, new Guid("b76f9378-fe54-453c-bc56-81a5a73617fe"), 4, "Maiores atque ea.\nConsectetur non est necessitatibus esse nesciunt beatae amet aut autem.", 2 },
                    { 35, new Guid("a35b7770-cf40-4644-a056-604378c4d677"), 4, "A aperiam adipisci ut quia et debitis.\nSit earum deserunt praesentium dolores et et.", 3 },
                    { 36, new Guid("e8a5cc5a-c3ad-482c-b4e4-21ef6e536804"), 4, "Cupiditate laudantium sit optio qui nostrum velit velit porro doloribus.\nSapiente est qui.", 5 },
                    { 37, new Guid("9b2724ea-3cfa-495a-a3e9-5da08ae72faf"), 4, "Ducimus nemo quis ex autem nihil.\nAut ipsa pariatur et illum.", 2 },
                    { 38, new Guid("b1a9b85a-fb20-4b8e-94fd-f802a44d4d6f"), 4, "Et aliquam rerum.\nAliquid nihil sint cupiditate odit cumque quam.", 3 },
                    { 39, new Guid("a71d33de-d7f4-433d-88db-6fd8b1898dde"), 4, "A reprehenderit tempora.\nDolor quia occaecati et quidem.", 3 },
                    { 40, new Guid("41375d72-9c23-4321-8f9c-03ecfaf8e145"), 4, "Maiores velit nulla incidunt est eum aspernatur commodi.\nMinus non quaerat voluptas rerum nesciunt sed aspernatur.", 3 },
                    { 41, new Guid("1661cc9d-65a4-44a9-96aa-44a270f5de53"), 5, "Qui eum accusantium.\nNeque corporis libero et.", 3 },
                    { 42, new Guid("1babacb4-2aa8-4db5-97d7-92c43b5765ed"), 5, "Velit id aut inventore.\nNesciunt numquam vel quasi soluta magni.", 4 },
                    { 43, new Guid("d3ba0372-d7f9-406a-99ab-f1875267f96b"), 5, "Sint fuga neque officiis veniam laborum impedit ut.\nRepellendus officia voluptate asperiores quia dolores.", 5 },
                    { 44, new Guid("139be156-aa63-4be1-a25e-c87b92864fdc"), 5, "Labore nihil ut.\nEt ut laborum ut placeat repellat magni nulla cum.", 3 },
                    { 45, new Guid("db12b3c9-098c-4fe6-99c4-322930b16c64"), 5, "Similique praesentium soluta asperiores et incidunt cumque rem nesciunt.\nQuae optio ullam.", 2 },
                    { 46, new Guid("dbe19636-3897-458c-af1b-53858e3fdb4f"), 5, "Omnis labore omnis eaque itaque iusto ut.\nVoluptate accusamus at porro dolor deserunt at totam quibusdam.", 1 },
                    { 47, new Guid("bcfbac12-71a8-496a-820f-3692842cc1aa"), 5, "Ratione sint libero quos voluptatum asperiores quo.\nDoloremque sit temporibus assumenda est minus nesciunt accusamus consequatur.", 2 },
                    { 48, new Guid("da0f5156-ed0b-47a9-ab64-96d6750afb6e"), 5, "Quae consequatur tempora voluptas rerum doloremque et facere.\nRecusandae rem qui nihil in.", 3 },
                    { 49, new Guid("019c8eca-f165-4c0b-909c-d7abab7e3358"), 5, "Excepturi tempora laudantium aspernatur atque nisi.\nLaboriosam et consequuntur excepturi.", 4 },
                    { 50, new Guid("322be082-f945-4356-905b-d8ee6837bbd0"), 5, "Eum cupiditate voluptatum est illo non.\nQuibusdam qui quo aut quia saepe nam aliquam.", 2 },
                    { 51, new Guid("3d5fb88c-c66b-4938-8e44-3bd00bf1ba84"), 6, "Quam sit voluptas provident aut error.\nAut natus impedit quasi necessitatibus hic non.", 3 },
                    { 52, new Guid("92585a4a-1034-4f79-a7b3-697b64f03df3"), 6, "Repellendus itaque qui quo a nihil pariatur cum ratione.\nMaiores voluptate quae in.", 5 },
                    { 53, new Guid("9da2d3c9-b207-4996-a58d-c6436ee06f41"), 6, "Aut officiis a.\nIpsam nesciunt et laboriosam et ipsam repudiandae perferendis similique qui.", 1 },
                    { 54, new Guid("54214871-3320-44f6-9601-b520cba44ea1"), 6, "Provident eum sint.\nConsequuntur ea est nulla delectus maxime in et aut enim.", 3 },
                    { 55, new Guid("e5feb0be-35f0-4d5f-8335-22bcdede548a"), 6, "In dolorem corrupti quisquam possimus sint.\nNihil officia nihil quidem.", 4 },
                    { 56, new Guid("5bbd8cf9-afba-41ae-a0dc-81af776c6b9a"), 6, "Aperiam cumque exercitationem aut ut dolor earum est occaecati maiores.\nModi dicta non dolores.", 1 },
                    { 57, new Guid("69a519f1-f5a6-4d32-bdb9-62691c48f4da"), 6, "Voluptas esse nobis sit dolorem delectus officia magni molestiae.\nAnimi facere et distinctio magnam.", 1 },
                    { 58, new Guid("7704c0dd-08a8-41f3-ae11-e622e5c969b4"), 6, "Nulla id repellendus dolore dolorem asperiores ut eligendi omnis sed.\nMagni placeat ipsa.", 1 },
                    { 59, new Guid("75d24ba4-ac15-4025-ba76-831574192cf6"), 6, "Iste alias et laborum sapiente debitis optio necessitatibus sint.\nQuae a consequatur at quas maiores delectus dolore mollitia qui.", 1 },
                    { 60, new Guid("8a0becd4-52dc-4948-af38-da7d7609a1e6"), 6, "Molestiae et qui enim provident corporis veritatis enim soluta.\nTempore aspernatur est delectus eos ullam eum.", 4 },
                    { 61, new Guid("8a20f66e-38f4-4a77-b171-cab5e32aef99"), 7, "Iusto illo assumenda voluptates.\nAliquid ratione est assumenda ullam enim ratione nisi.", 4 },
                    { 62, new Guid("50afea96-3d1c-48dc-bfe7-dde243ccda66"), 7, "Quia in quia ipsa fugit.\nNulla dolor ut impedit nisi harum non culpa rerum.", 1 },
                    { 63, new Guid("4254f7d0-ddd2-4dae-b3ef-2d6685f86d7f"), 7, "Non minus velit quam odit commodi fugiat.\nReiciendis molestiae ea modi rem eum et ullam possimus.", 1 },
                    { 64, new Guid("a092f145-16fd-41f5-9a32-035a30961c28"), 7, "Aliquam odit laborum dolores.\nSapiente aut assumenda aspernatur vel eos occaecati fugiat ipsa aut.", 5 },
                    { 65, new Guid("94c9097d-8aa8-4f85-924c-3ed1209f4d98"), 7, "Minus quia nemo ut in sint ut.\nFugit sunt sit qui est similique cumque velit recusandae.", 5 },
                    { 66, new Guid("03ca4f0b-6115-45b6-b545-d489ff235262"), 7, "Et mollitia voluptatem modi.\nReiciendis at voluptas ea voluptate.", 4 },
                    { 67, new Guid("bae85d6d-173d-4bf6-b69f-c46ed40ff58b"), 7, "Voluptatem officia repellat dolorum alias tempora autem et.\nQuae minima cupiditate esse.", 1 },
                    { 68, new Guid("4fa12f7a-7813-4526-b87d-fe881b262d97"), 7, "Quam consectetur et.\nLibero sit et.", 5 },
                    { 69, new Guid("ef28a50d-3b5b-456e-a06b-87cc6114c3e8"), 7, "Eum aperiam numquam dolorum expedita ullam nihil voluptas ut ullam.\nRerum facere veritatis quia aut minima ut quia voluptas.", 1 },
                    { 70, new Guid("217496e0-8e45-4e30-92b9-239626e4ec0f"), 7, "Cum alias et est sed et sed.\nEt quisquam ducimus porro laborum aperiam assumenda.", 3 },
                    { 71, new Guid("9392bec3-e2eb-425a-bfb0-f199795b937f"), 8, "Et quidem repudiandae nemo officia sapiente.\nTempora est deserunt quae veniam dolores fugit ut culpa odio.", 4 },
                    { 72, new Guid("e02c146b-925a-4ffa-bafc-7bb6477c05af"), 8, "Et ipsam excepturi expedita et aut odio.\nUt labore beatae.", 5 },
                    { 73, new Guid("ec14b35c-cb9c-4d6c-aef0-1d2021173c41"), 8, "Velit sint sed fugiat eum et.\nEt quos non laudantium.", 5 },
                    { 74, new Guid("9a5aea39-e138-4410-b331-f90795891e49"), 8, "Quas ea sed cum quis dolor.\nNecessitatibus iure ut quam.", 1 },
                    { 75, new Guid("6900c3af-e9de-457a-bedc-d8bcebde914e"), 8, "Placeat commodi et nisi quia nobis facilis eos.\nNecessitatibus voluptatum et rerum soluta qui doloribus excepturi quo.", 3 },
                    { 76, new Guid("4af100c7-7914-4509-9c57-fde471e93deb"), 8, "Omnis sit ullam.\nEt ab consequatur nesciunt magnam magnam praesentium nemo minima blanditiis.", 1 },
                    { 77, new Guid("3944306b-ec30-4535-b779-9529901f01ba"), 8, "Commodi non minima quia consectetur eum veritatis quibusdam porro sapiente.\nEarum qui quia dolorem rerum fugiat aut.", 4 },
                    { 78, new Guid("1f222984-cb13-4bab-9a43-d482bf9e3fcf"), 8, "Possimus neque dicta repudiandae voluptatem ipsum asperiores.\nNeque dicta vel ea impedit sunt ut repellat perferendis.", 1 },
                    { 79, new Guid("f6d004f5-3103-4bf9-9cb7-deff12ff0c47"), 8, "Et a odit aut quos.\nIn nostrum facere consectetur velit culpa.", 2 },
                    { 80, new Guid("3999a954-6d90-44f1-8da3-6802a39a15e0"), 8, "Consectetur modi explicabo omnis temporibus pariatur explicabo nesciunt.\nQuisquam dolorum non ducimus.", 5 },
                    { 81, new Guid("7b572589-33a8-4303-aa46-96a4e91b8247"), 9, "Exercitationem reiciendis iste itaque.\nVoluptatem magni quia asperiores temporibus officia nesciunt eos.", 1 },
                    { 82, new Guid("c2e5c276-ce68-4e19-b68a-0fe44d70d476"), 9, "Voluptatem aut ea sunt perspiciatis voluptatibus asperiores sit.\nEt vel fugit corrupti sint.", 4 },
                    { 83, new Guid("0c324d25-089f-4204-9867-782eb83a722a"), 9, "Et quod a quia ipsam dolor.\nQui doloremque soluta.", 5 },
                    { 84, new Guid("6d934861-94e0-4e40-83a4-9006b9410223"), 9, "Voluptatem labore eius commodi exercitationem.\nQuo eos quis provident eum explicabo maiores id ut.", 1 },
                    { 85, new Guid("58d07003-f060-464b-b207-e49978ffc9ef"), 9, "Et excepturi eos incidunt aut nihil natus.\nDolor a laudantium dolorum magni iure itaque.", 5 },
                    { 86, new Guid("6e012770-a0d5-4764-9993-7accf40b89e0"), 9, "Ullam quos reprehenderit doloribus velit iste.\nAccusantium rerum voluptate aliquam soluta ullam voluptatem molestiae est.", 2 },
                    { 87, new Guid("c55f91f9-47f5-477f-b612-bb18c924a9ec"), 9, "Et temporibus aut laboriosam itaque accusantium corporis voluptatem.\nQuo dolor excepturi minus libero beatae unde labore voluptatum.", 2 },
                    { 88, new Guid("487464ce-615d-4dcc-bdf5-d024369c3f12"), 9, "Aut impedit inventore natus ut voluptatem provident.\nMolestias qui ut asperiores labore iste incidunt.", 3 },
                    { 89, new Guid("3747a03d-a68a-4e8e-8bd0-d55306bb88b1"), 9, "Eos tempora hic voluptatem provident atque tenetur.\nAut sit voluptas nisi sint vel officia voluptas velit dolorum.", 4 },
                    { 90, new Guid("ded3e2cb-5300-4994-bb88-dcd6a9ae6cfc"), 9, "Doloribus ut quasi veritatis eum dolores sit iusto quos quasi.\nDolor nam fugiat.", 5 },
                    { 91, new Guid("df566bed-ad40-4145-90ae-52c7c7d77463"), 10, "Consequatur soluta aut dicta accusantium dolores architecto quasi omnis ut.\nConsectetur placeat iste natus et omnis exercitationem aut.", 3 },
                    { 92, new Guid("99635f69-f6cd-4d7f-9fd5-beb937a91777"), 10, "Similique omnis eius.\nVelit laudantium laboriosam minus neque vel amet provident provident deleniti.", 1 },
                    { 93, new Guid("e24b01ac-97a2-4bdc-999e-54bed3992164"), 10, "Consequatur dolore temporibus nihil ut mollitia qui.\nOptio sunt quo.", 4 },
                    { 94, new Guid("1a0ea8b0-0684-4ab6-b657-06c2bc2a7b66"), 10, "Corrupti ut et exercitationem quia cum accusamus qui.\nQuia maiores sit eligendi hic cupiditate qui laudantium earum.", 4 },
                    { 95, new Guid("5f741802-fa34-44a7-a8c8-30030c1262d5"), 10, "Culpa eligendi iure.\nEa laudantium ducimus aut tempore eos voluptate nihil dolores.", 5 },
                    { 96, new Guid("26851311-c153-417a-921e-1425ac20d68f"), 10, "Cum quia consequatur autem.\nQuod quia et mollitia culpa similique.", 4 },
                    { 97, new Guid("9a2d3534-d5c2-4ce7-9539-a3767535d14b"), 10, "Voluptas et voluptatem accusantium libero ut nobis exercitationem optio quia.\nItaque nisi aperiam qui.", 2 },
                    { 98, new Guid("d3ecf237-7f52-482b-8786-0a4ac236a237"), 10, "Sunt animi ullam maiores.\nAb repudiandae mollitia magnam.", 3 },
                    { 99, new Guid("39b5336b-11ec-4441-af42-53fa632b58b5"), 10, "Quibusdam qui voluptatem vitae ut rerum asperiores consectetur quibusdam.\nQuae in ea delectus mollitia ut id voluptatibus.", 5 },
                    { 100, new Guid("742442d5-f8b3-4302-9b80-4d80fbd95337"), 10, "Reprehenderit saepe facere qui minima.\nAut deserunt et officia nisi enim unde sint omnis.", 1 },
                    { 101, new Guid("4b8f27e9-8884-4b50-8c04-2f52247293cd"), 11, "Commodi hic quas iure eum qui numquam magni molestiae quaerat.\nEsse repellat ea nisi velit rerum aut earum.", 2 },
                    { 102, new Guid("5bab7b1f-7b95-4753-afb0-68d65d3e3c0d"), 11, "Soluta illo reiciendis iusto eveniet velit voluptatem molestiae quisquam.\nIpsum earum natus qui velit ad tenetur dolores dignissimos.", 3 },
                    { 103, new Guid("401c4ce5-848d-4e05-9cd3-f079d34f877a"), 11, "Culpa hic fugit ut.\nMollitia quia aut nihil et impedit minus ad.", 3 },
                    { 104, new Guid("92695f10-d0d7-4746-aeb8-58377989934b"), 11, "Voluptas maxime minima nemo ducimus blanditiis est eum perferendis.\nEveniet quis iusto dolores ad illum dicta omnis ipsum fugiat.", 3 },
                    { 105, new Guid("b463b9ee-29f6-430a-b174-8d72e9aa3996"), 11, "Et impedit incidunt in nobis eum perferendis est temporibus eum.\nDoloremque architecto excepturi dolores reiciendis sit a occaecati placeat.", 3 },
                    { 106, new Guid("cb30d029-c885-4592-9f52-4d9f44d60c7c"), 11, "Repudiandae ipsum aspernatur fugit explicabo non blanditiis iure quibusdam voluptas.\nNobis maiores numquam velit consectetur voluptas.", 2 },
                    { 107, new Guid("797091ac-3263-499f-b6b7-343937898d49"), 11, "Qui consequuntur et dolores consequuntur dicta consectetur.\nMaxime odit voluptas.", 4 },
                    { 108, new Guid("f17a4ff4-6678-478a-bbd0-6e90ee8332ec"), 11, "Nobis corporis aperiam id praesentium.\nExercitationem quia eum.", 1 },
                    { 109, new Guid("9dd078f0-1d00-4193-93f9-505ed5a6054a"), 11, "Illo vero ut odit quam est amet ut consequuntur.\nEaque tempora itaque quo dolorem.", 1 },
                    { 110, new Guid("7d288a75-0e18-4ae7-bd71-593f2056bfe8"), 11, "Aspernatur autem accusamus atque cumque possimus asperiores.\nDelectus aut enim.", 3 },
                    { 111, new Guid("e7ce828e-792c-464c-83dc-9ff916e6e0f9"), 12, "Suscipit sapiente molestiae dicta cum aut nostrum voluptatibus possimus repudiandae.\nDolore aspernatur quis itaque sed dolor sed necessitatibus.", 5 },
                    { 112, new Guid("1332e559-fa90-4da2-b57a-1710b7f08eb8"), 12, "Odio cum perferendis.\nQui occaecati necessitatibus voluptatem sint eum laudantium.", 1 },
                    { 113, new Guid("718aafd6-f4f2-4053-a9bf-cc3dca259be1"), 12, "Excepturi neque quas hic modi autem ut hic alias sequi.\nAliquid exercitationem iste aut quisquam ea et hic accusamus dolorem.", 4 },
                    { 114, new Guid("bacc9741-e48c-4503-9e1e-5a1131ed5223"), 12, "Voluptatem ut architecto sunt recusandae soluta aliquid sint vitae.\nVeritatis numquam deserunt repudiandae velit ab.", 4 },
                    { 115, new Guid("8809d024-f7df-49f6-b700-60619f052fa3"), 12, "Qui deserunt architecto omnis necessitatibus est sunt delectus non.\nNon soluta dignissimos commodi ea consequuntur soluta eius.", 5 },
                    { 116, new Guid("fa4b84ea-a49c-45cf-9dba-79818ca7b2ac"), 12, "Autem nihil aut quos dolorem.\nDolorum beatae ea ducimus.", 3 },
                    { 117, new Guid("4bb4c9ca-d496-421f-9be2-8b363962aedc"), 12, "Necessitatibus neque sunt ut minus eaque quas aliquam illo aut.\nFacilis aut commodi aspernatur eos magnam omnis sit voluptas a.", 5 },
                    { 118, new Guid("4e713fcc-cb35-48f9-b497-8add8b897ace"), 12, "Ea aut et quam est enim eius et odio aliquam.\nOptio fuga dolor labore cupiditate deserunt odit.", 3 },
                    { 119, new Guid("5ca2e291-7f53-46fc-bb68-c6da06e3eb17"), 12, "Incidunt vel voluptate nihil quia nihil veritatis necessitatibus rerum sequi.\nIncidunt placeat odit aliquam praesentium qui ut expedita.", 3 },
                    { 120, new Guid("d04f0848-d791-4bab-a130-1460bfcef0a4"), 12, "Cum quam sint velit minima enim.\nPossimus fugiat provident doloremque animi adipisci id.", 1 },
                    { 121, new Guid("3a61da82-3aed-4139-b94b-80ea40347f16"), 13, "Impedit dolorem unde hic minus.\nRepudiandae minus totam eligendi.", 4 },
                    { 122, new Guid("ae4d8d12-8731-45a1-a5ba-25882566c0cc"), 13, "Ex aperiam aspernatur beatae ipsum cumque id voluptatum iusto.\nMagni minima ut maxime alias nam.", 1 },
                    { 123, new Guid("7f978a7c-0c2b-4122-969a-af0e5abd9e88"), 13, "Corporis eius voluptas minus.\nVoluptas esse quas eligendi enim amet sequi dicta quidem.", 4 },
                    { 124, new Guid("ea92f0a6-aa50-4e98-b190-25ce023d3f49"), 13, "Sequi aut odio est dicta perferendis dicta cum in illum.\nEt nam laboriosam.", 3 },
                    { 125, new Guid("0720402a-50da-4d99-bf27-fac12ed6faa6"), 13, "Possimus assumenda eligendi neque.\nNostrum vel dolore rerum iste qui occaecati.", 5 },
                    { 126, new Guid("d73dacdf-7423-4396-9e03-f1de32c8bc6c"), 13, "Veniam sed beatae molestias adipisci odio eum necessitatibus.\nCorporis commodi dolorum.", 1 },
                    { 127, new Guid("43f01386-e162-4dbe-9e7c-e8739ae19fc5"), 13, "Doloremque earum repudiandae.\nVoluptatem voluptatum ut laudantium.", 5 },
                    { 128, new Guid("bc5b750c-f6a5-410c-b84b-de58efebfb5b"), 13, "Culpa molestias dolores maxime voluptatem voluptate.\nNumquam est eos est explicabo voluptatem rerum qui et.", 1 },
                    { 129, new Guid("f062c325-5864-49d0-a2dc-400a260d4503"), 13, "Quae fuga error.\nEt sed debitis laboriosam repellat maiores hic excepturi similique.", 4 },
                    { 130, new Guid("3b2f89f2-3019-4d8e-9b15-5ec9801b15de"), 13, "Ex doloremque vel accusantium molestiae sunt.\nModi sed sit error aut non dignissimos autem voluptate et.", 1 },
                    { 131, new Guid("c05b0f4c-db43-4952-9ab1-a930d4e85b4f"), 14, "Aliquid quae et.\nQuae nulla tenetur.", 3 },
                    { 132, new Guid("5b87ef6c-3464-4952-bc29-8090dd167e93"), 14, "Ullam quidem debitis quod et suscipit provident.\nQuis enim velit iure voluptate sed amet cum voluptatem.", 3 },
                    { 133, new Guid("83d82498-86a7-4298-b548-0a56436993a9"), 14, "Voluptatum sit sit minus.\nQui qui aperiam omnis pariatur aut.", 5 },
                    { 134, new Guid("506b2159-35af-453e-93f6-41a95325c984"), 14, "Et molestiae labore aut et quis vero libero et.\nQuisquam est nesciunt quo occaecati aliquam quia aliquid sapiente.", 3 },
                    { 135, new Guid("9a736ce9-1bed-44c3-b570-cc465b4f9ee7"), 14, "Eveniet distinctio fuga omnis architecto sequi illo dicta.\nVoluptatum debitis tenetur ut.", 3 },
                    { 136, new Guid("c8e0b92a-c149-4a50-9f2b-0fbecbe3f276"), 14, "Et est ipsam aut repellat nemo praesentium aliquam et quis.\nMagni fugit tenetur perspiciatis dolore voluptates sit repellat vitae.", 3 },
                    { 137, new Guid("201e7c1c-4772-4c1c-9d13-83501cccc894"), 14, "Consequatur velit quis voluptatibus.\nAspernatur exercitationem harum.", 1 },
                    { 138, new Guid("d728a4db-5854-4f53-87cb-acc940cced94"), 14, "Voluptas vero ipsa provident quis ratione.\nVelit quis dolore eos voluptates.", 2 },
                    { 139, new Guid("a6c4d680-b9ac-42c0-8a1d-58fece65ac92"), 14, "Ut distinctio et debitis nihil consequatur consequatur.\nSed officia sunt consequuntur iure labore ea.", 3 },
                    { 140, new Guid("54db873d-9cc4-4cec-aa69-4bc6d8a7eb6d"), 14, "Dolorem sint ex perspiciatis facilis quos.\nAut est officiis et quaerat laborum quia.", 1 },
                    { 141, new Guid("ae789ad4-ee86-4b10-9fc8-2404f5af2a2d"), 15, "Error quo voluptatem dolor et optio magni veniam deleniti quas.\nAtque et eligendi.", 3 },
                    { 142, new Guid("62bfa265-d4de-4d04-8c32-d31e6da7c6f2"), 15, "Distinctio qui optio quibusdam error repellat.\nBlanditiis doloribus sed beatae.", 5 },
                    { 143, new Guid("6ec13a44-760f-4fc3-9c9b-bdbba22b08fe"), 15, "Iure id beatae alias est consequatur.\nEst et ipsam maxime aut modi delectus.", 3 },
                    { 144, new Guid("20ba0e4e-cdb4-4509-887b-2dc1c540596b"), 15, "Culpa cupiditate doloremque est corporis qui.\nCupiditate dicta repudiandae explicabo in et dolore sit.", 5 },
                    { 145, new Guid("931c0795-3977-4b45-b110-8b13367c40c9"), 15, "Delectus eum in non quas.\nEarum dolorum rerum error fuga vero.", 5 },
                    { 146, new Guid("612df2d6-ae37-4787-a13d-8cd84c1c18ca"), 15, "Esse provident a ut sapiente quis magni perferendis ad corrupti.\nMagnam eligendi in fugit deserunt doloribus autem omnis ut animi.", 2 },
                    { 147, new Guid("74f0c2f1-fd0b-4273-8fd6-60c55d34445f"), 15, "Quis atque consequuntur voluptatibus sunt eos.\nIncidunt sit deserunt aliquid reprehenderit assumenda hic.", 4 },
                    { 148, new Guid("008b0e24-be12-498e-9068-666a5c72ee22"), 15, "Est molestiae laboriosam nihil dolores.\nNulla officia inventore voluptates cupiditate accusantium harum et.", 1 },
                    { 149, new Guid("112307f7-fbdb-4ed6-b4b5-3412c5aaf5ea"), 15, "A magni iusto.\nQuasi vel odit ut asperiores quasi architecto sunt et.", 2 },
                    { 150, new Guid("bc9f2d64-af77-42ee-9f7b-fd9c3357678e"), 15, "Nihil consequatur quam quia similique qui occaecati explicabo.\nSit aliquam rem aut odit qui fuga.", 2 },
                    { 151, new Guid("d1568f83-1fee-4d79-adb2-b2533c629ded"), 16, "Maiores quia rem est aperiam.\nDicta esse non molestiae.", 4 },
                    { 152, new Guid("24adc13a-fa56-4010-a9cf-20812283d4ac"), 16, "Reiciendis aut placeat.\nModi non rerum ullam id omnis optio voluptatem minima ut.", 3 },
                    { 153, new Guid("416c25f4-afca-43bb-aad8-7670cd14a83a"), 16, "Quas excepturi aliquid tenetur et quasi.\nQui blanditiis ab vel voluptate vel non.", 3 },
                    { 154, new Guid("3cc2151a-7e5b-47fd-9b59-2355efe14c98"), 16, "Quia placeat eaque perspiciatis porro voluptatibus qui.\nRerum commodi dolorem.", 4 },
                    { 155, new Guid("a9afc771-9464-4794-af29-f225ea118ed5"), 16, "Aut et dolorem.\nEt deserunt at aperiam veniam soluta.", 4 },
                    { 156, new Guid("154aa595-7f98-4456-b2fc-f66eae9e5ff0"), 16, "Ex fugiat molestiae esse dolorem autem qui.\nAutem eius consequatur.", 1 },
                    { 157, new Guid("a72feac1-daf7-4e9b-afba-81d1bee5639c"), 16, "At recusandae distinctio aspernatur.\nSit labore labore reiciendis est dolore ullam exercitationem quod quis.", 1 },
                    { 158, new Guid("5e7349a4-d05a-4f87-bb83-f1a880347bcd"), 16, "Voluptatem molestiae et eius qui optio et et.\nMaxime libero laboriosam omnis ab occaecati dolore sit possimus et.", 4 },
                    { 159, new Guid("bd25bbf7-0e75-4e1f-b4d7-6660f5117230"), 16, "A dolore quaerat ipsa culpa eligendi commodi ipsa necessitatibus.\nMinima excepturi ad.", 2 },
                    { 160, new Guid("ccbe44ab-1d9e-423e-b649-92b1dd9ab588"), 16, "Voluptatum aut qui minus.\nExcepturi enim iste est.", 5 },
                    { 161, new Guid("8812951d-b82a-4b29-bbfb-1afbfedba676"), 17, "Harum deleniti cum nulla.\nAlias porro ab assumenda molestias.", 4 },
                    { 162, new Guid("c42054e0-7931-402c-9d5a-22c7f1477e03"), 17, "Accusantium et iusto praesentium illum aperiam molestiae est.\nOccaecati sunt et ullam temporibus possimus dolorum soluta.", 4 },
                    { 163, new Guid("1e681ce8-f8f2-4df2-abf4-3ed5b376044d"), 17, "Delectus aspernatur nostrum aut non a eaque voluptatem.\nFacere voluptatem amet enim quasi tempore corporis.", 2 },
                    { 164, new Guid("e7021a08-bc85-45be-ba8d-d6e4f75f0721"), 17, "Quae aliquid ad vero exercitationem quam praesentium.\nAut numquam nulla rerum sed saepe officia quidem facilis quod.", 2 },
                    { 165, new Guid("cd9f5594-a24c-4776-abcf-33032b5f5081"), 17, "Omnis ut earum sed veritatis.\nLaboriosam nobis eum eum fuga voluptatem eum.", 2 },
                    { 166, new Guid("91b734ca-46ea-4031-881e-565554e8bd64"), 17, "Commodi quis consequuntur minus unde rerum consequuntur et.\nNisi soluta commodi quae vitae molestias.", 3 },
                    { 167, new Guid("e46d2835-9115-4936-ba09-936b663198ba"), 17, "Enim enim aut praesentium.\nDeleniti velit sed cupiditate.", 5 },
                    { 168, new Guid("1a653afa-0563-456f-92c1-e9dad4563316"), 17, "Est odio minima eos aut rerum enim possimus corrupti.\nSapiente magni non et provident ex in saepe aut molestiae.", 2 },
                    { 169, new Guid("75378a99-3044-4cae-916a-dfc307ecfa6a"), 17, "Et ipsa et reiciendis quia quis ab sint ut.\nCorporis voluptas quis vitae sit nesciunt sint fugit amet fugiat.", 2 },
                    { 170, new Guid("d6f8f227-d98c-41df-96ba-955bcf5905a0"), 17, "Eligendi doloribus omnis libero minus alias.\nDoloribus et vero ipsum facilis nulla et est voluptas labore.", 2 },
                    { 171, new Guid("cb6de87d-8c87-4831-ac55-f7f68d309a9b"), 18, "Quam et id.\nAutem pariatur ratione.", 5 },
                    { 172, new Guid("ec063e4c-6c43-46d0-8761-9a35943b2637"), 18, "Cum aut eveniet corrupti eos voluptas reiciendis.\nUt facilis qui non commodi aut alias ex.", 4 },
                    { 173, new Guid("93a3f99e-323f-48ba-bd20-f7f6f37f53a3"), 18, "Ipsam mollitia explicabo sint placeat.\nVoluptas magnam voluptas molestias consequatur id.", 2 },
                    { 174, new Guid("ee319e7e-5a8c-4868-9db1-59ab9190af55"), 18, "Necessitatibus enim numquam sint animi aut dolorum perferendis non inventore.\nDistinctio excepturi atque perspiciatis voluptatibus debitis eum ut.", 4 },
                    { 175, new Guid("de68b3b2-f122-40c0-a706-731714ded940"), 18, "Iusto quos id qui.\nQuod nihil ad dignissimos enim aliquam sint laboriosam.", 4 },
                    { 176, new Guid("82da66a4-5d76-424b-8b52-3b42c9c0041e"), 18, "Unde fugiat labore qui et quia.\nSoluta recusandae non eum assumenda distinctio.", 4 },
                    { 177, new Guid("04e1187e-729c-4716-b222-231dd3671d8d"), 18, "Repudiandae quibusdam inventore qui doloribus.\nIpsa sed in sint quis itaque rem itaque repellendus.", 2 },
                    { 178, new Guid("8cd408e2-d44e-41e0-9e7f-57c6b823aae4"), 18, "Non at temporibus.\nOdit eos quisquam nulla.", 3 },
                    { 179, new Guid("fe04d959-8022-4431-99a4-3fc384954e22"), 18, "Eum sequi reiciendis cumque doloribus soluta nesciunt qui nisi.\nRem sed at eveniet ex est fugit sunt in et.", 4 },
                    { 180, new Guid("39c01119-c6dc-495b-b275-69e833377248"), 18, "Necessitatibus et qui velit assumenda itaque et.\nPorro consequatur error ab nostrum tenetur consequatur.", 5 },
                    { 181, new Guid("38e6b432-e5cb-4ab6-b012-db35d1aa83b3"), 19, "Architecto ut possimus reprehenderit.\nVoluptas temporibus sequi aut sed ut.", 2 },
                    { 182, new Guid("56ec7729-bfd5-4f1b-9b56-6aea1397efb7"), 19, "Odit rerum voluptatibus omnis ut veniam ratione omnis sit.\nMagni voluptatem adipisci.", 2 },
                    { 183, new Guid("82ef3d8a-b7a1-4a57-a367-09c181974563"), 19, "Ut est id molestias explicabo esse commodi.\nEos et veniam.", 4 },
                    { 184, new Guid("eb418921-ca10-4193-acc0-5a495cd323f5"), 19, "Odio maxime iusto at ea omnis.\nSed odio qui eius repudiandae qui quas.", 1 },
                    { 185, new Guid("d7c997c0-e79a-41b2-8464-702dcf6064ec"), 19, "Fuga consequatur beatae ut voluptatibus veniam est.\nAspernatur tempore illum commodi cum quasi minus est molestiae.", 1 },
                    { 186, new Guid("b2180b5b-45f1-4a3e-905b-b7c054a93f98"), 19, "Eos culpa ut nesciunt et iste minima.\nEius tempora praesentium dignissimos ea necessitatibus neque inventore voluptate.", 3 },
                    { 187, new Guid("5f277e77-b3db-4c90-afef-cc9d01fd55da"), 19, "Nihil beatae voluptatem tenetur quas officia accusantium omnis hic aut.\nEum et dolorem vero voluptas occaecati consequuntur.", 1 },
                    { 188, new Guid("a610c778-1547-43a1-aaa9-8906dc3dde92"), 19, "Earum in molestias voluptatem rerum cupiditate.\nNostrum et nihil totam.", 2 },
                    { 189, new Guid("9b16d613-de1e-44a6-a2f1-21ed6ee4250b"), 19, "Aut est et a eligendi.\nReprehenderit magnam voluptas quibusdam voluptatem consequatur.", 5 },
                    { 190, new Guid("88cffdbc-0935-4093-92dc-1f56812e8b88"), 19, "Aperiam nam voluptatum fugiat aut tenetur aut.\nQuidem sint facilis architecto doloribus reprehenderit aut perferendis libero nobis.", 3 },
                    { 191, new Guid("f2e73352-e84a-4a2c-bba4-1b871121bfbc"), 20, "Ut accusantium harum vero ut doloribus velit.\nSit odio officiis fugiat sapiente eos omnis vitae est assumenda.", 2 },
                    { 192, new Guid("fce2c2bf-52d2-43e9-80d4-59e34cdfe02d"), 20, "Iste atque suscipit molestiae nisi voluptatem fuga.\nEa illum earum.", 4 },
                    { 193, new Guid("d0e6a75a-9fef-4958-8f8e-fa1b80fd9823"), 20, "Velit soluta molestiae recusandae dolores.\nEos porro ut quia laborum.", 2 },
                    { 194, new Guid("355cec0f-b8db-4b87-b21c-fe5bc8610e47"), 20, "Voluptates expedita et ea dolor architecto aliquam ut qui.\nIn provident dignissimos voluptatem magni qui et consequatur et qui.", 3 },
                    { 195, new Guid("264fa397-36ba-4199-ada2-201c9f75e7da"), 20, "Consequatur consequatur beatae minus eos blanditiis.\nEligendi nobis quisquam iste.", 2 },
                    { 196, new Guid("ebfb2e34-3457-43c4-95fc-ca3ffe9f32ea"), 20, "Rem ullam quaerat et odit libero voluptatem laborum.\nSuscipit qui officiis.", 4 },
                    { 197, new Guid("158ec81e-8b89-4841-803e-4e690feb7fa2"), 20, "Labore laborum magni voluptate quod accusamus vero.\nEx dolorum accusamus quae est facilis.", 3 },
                    { 198, new Guid("c1cac505-bfee-4094-a59a-f8c6e3c9d707"), 20, "Nihil eaque fugiat.\nQuasi quo est dolorem est.", 3 },
                    { 199, new Guid("93f71c32-0f2f-47ea-b528-d6b1906a4362"), 20, "Ipsum voluptatum molestiae voluptatum at soluta tempore nemo recusandae ipsa.\nNostrum earum qui fugit omnis cum ipsum dolorum.", 2 },
                    { 200, new Guid("76507df6-7d8b-4b4e-86ad-c538021d2e63"), 20, "Quaerat porro voluptatem adipisci assumenda saepe.\nQuod tenetur nostrum libero fuga aut laboriosam et.", 1 }
                });

            migrationBuilder.InsertData(
                table: "Reactions",
                columns: new[] { "Id", "Guid", "PostId", "UserId" },
                values: new object[,]
                {
                    { 1, new Guid("42be93e3-43ae-4ddf-b1f1-0ed56f85631c"), 1, 2 },
                    { 2, new Guid("ee7ffa58-7017-4c50-b372-2adb99598016"), 1, 1 },
                    { 3, new Guid("fb71cd69-0b5a-48d9-b7e2-ed3cfc4490c0"), 1, 1 },
                    { 4, new Guid("c9ac5464-98c9-4701-bbf0-d0d7cbef6415"), 1, 5 },
                    { 5, new Guid("8440f1c4-b45b-4111-8aff-ca3db755a707"), 1, 3 },
                    { 6, new Guid("e626c63f-7834-42f2-8d6b-a410d844939d"), 2, 1 },
                    { 7, new Guid("69e129e0-10f8-4def-ab79-979fdcf5a9b9"), 2, 5 },
                    { 8, new Guid("91568589-bb1f-4fa6-ad5b-0232dbf32a37"), 2, 5 },
                    { 9, new Guid("1f0441bf-3c36-4748-8e4f-5f6ee219c73a"), 2, 2 },
                    { 10, new Guid("2bda3884-8a80-4ffa-b359-61c0607dce71"), 2, 1 },
                    { 11, new Guid("3c022159-0e8d-4063-8796-4051d7d1fe75"), 3, 2 },
                    { 12, new Guid("3a7e5991-ef1f-4d7e-8d24-96cc0f4b4f7c"), 3, 4 },
                    { 13, new Guid("17af84b7-29df-4115-be23-7591c13f5ec6"), 3, 5 },
                    { 14, new Guid("43f11eb4-f78e-4d58-887f-8c7eed92aee6"), 3, 4 },
                    { 15, new Guid("f984baa4-acce-439a-ab89-3b3c0c1af98c"), 3, 2 },
                    { 16, new Guid("d2f7b467-79e4-49ef-b36f-2cc4b793ac03"), 4, 4 },
                    { 17, new Guid("6f545cdb-1ed5-4250-b098-1df152438f66"), 4, 5 },
                    { 18, new Guid("e67104b7-2b88-41ac-874e-680fa4242aa6"), 4, 4 },
                    { 19, new Guid("0745c0cf-a0ce-44ab-b278-6805bbe526a4"), 4, 1 },
                    { 20, new Guid("a9bf17ff-d53a-4868-9b36-9642a561b108"), 4, 2 },
                    { 21, new Guid("c32d7885-f346-4aee-9452-0ad99d809a65"), 5, 3 },
                    { 22, new Guid("969c8984-1328-45d7-95d8-f40892858cb6"), 5, 5 },
                    { 23, new Guid("3906610e-f836-4c52-8cf8-9ce63b1e2f1e"), 5, 5 },
                    { 24, new Guid("eb56a0c4-d6bd-47c3-929b-111a4ebc12a1"), 5, 1 },
                    { 25, new Guid("57f3bdd4-26fd-469b-a0d5-8de03a906288"), 5, 2 },
                    { 26, new Guid("58d3c803-5965-497f-af59-ea666df37e44"), 6, 4 },
                    { 27, new Guid("1c1190da-b80d-4ae7-a0c5-1c892e0f2d8d"), 6, 3 },
                    { 28, new Guid("8cdb2dc6-d182-4202-8ceb-c0d736804171"), 6, 1 },
                    { 29, new Guid("2c8297bd-c8a6-40fb-b9ac-16b26e686f88"), 6, 2 },
                    { 30, new Guid("5210d91f-197b-4f4f-bc55-9c1b121479d5"), 6, 4 },
                    { 31, new Guid("71463791-088f-4359-8aad-209eeecd742a"), 7, 4 },
                    { 32, new Guid("b9c6791b-d715-4092-91cb-03e2629995d3"), 7, 3 },
                    { 33, new Guid("1dbdf2cf-fed3-4b6d-af7a-489d390c53a7"), 7, 2 },
                    { 34, new Guid("4714a15b-b6f5-484e-be14-03627c627e85"), 7, 5 },
                    { 35, new Guid("a3149310-f3a7-4fa9-8001-0f36a61ef7f7"), 7, 3 },
                    { 36, new Guid("b8ce7c84-469f-45cb-aa7e-469b660b97cb"), 8, 4 },
                    { 37, new Guid("c58433fd-91a0-41f8-8822-8a3c8eda4c0f"), 8, 2 },
                    { 38, new Guid("590e94f7-455f-4349-9367-264d0dbbabe5"), 8, 5 },
                    { 39, new Guid("cd4fb65d-1475-4837-8e76-98498523708a"), 8, 1 },
                    { 40, new Guid("7bd438e5-be43-4a54-8ffd-bcf8e9b82e8e"), 8, 5 },
                    { 41, new Guid("3c72a09c-3bc3-4ff9-b552-d7ac2289a6b2"), 9, 2 },
                    { 42, new Guid("392b5e23-b454-45cf-b0bc-221bd4797e4a"), 9, 5 },
                    { 43, new Guid("4e6d9ea4-a5ae-4fd8-9079-00e66555f856"), 9, 1 },
                    { 44, new Guid("2b63699d-97cc-406e-ba86-20f8ecba902f"), 9, 1 },
                    { 45, new Guid("4760bcc7-27e8-470b-a109-fbc115f79dde"), 9, 5 },
                    { 46, new Guid("e07e3337-7481-4b8f-90d7-97ffc8871237"), 10, 3 },
                    { 47, new Guid("2d4008ab-d3fb-4a37-8b4c-8efebc5d559e"), 10, 5 },
                    { 48, new Guid("d275c83c-3713-4d65-af58-329e3da09ac6"), 10, 5 },
                    { 49, new Guid("a70dafb3-b568-4fd7-901e-83cb52b2bb42"), 10, 2 },
                    { 50, new Guid("96d15e88-e777-49b3-a055-15c4d489ae1b"), 10, 2 },
                    { 51, new Guid("99bff37b-fdb2-46bb-8317-4081cd36698f"), 11, 2 },
                    { 52, new Guid("478756a2-33aa-42d3-b99f-224e2aa134d9"), 11, 2 },
                    { 53, new Guid("8300e43d-9956-48de-b33c-5d84f93e74ea"), 11, 2 },
                    { 54, new Guid("5b0a49bc-fcce-45e2-b75d-08b192e03c8d"), 11, 4 },
                    { 55, new Guid("48809832-e4c4-4da5-a409-b72b73c5db49"), 11, 1 },
                    { 56, new Guid("8dd827ef-82be-4659-9bf0-b17e5c69e500"), 12, 4 },
                    { 57, new Guid("40d341c7-c8ca-4a3a-b251-b746f9ecdabe"), 12, 4 },
                    { 58, new Guid("8ff6bc2a-a330-40db-941b-d90d00282d3f"), 12, 5 },
                    { 59, new Guid("50b6950f-75e6-4170-9298-ca694615d27a"), 12, 5 },
                    { 60, new Guid("9e07f867-566f-46bf-ab5d-df4a3fa9cf88"), 12, 1 },
                    { 61, new Guid("fca5e8d5-91f8-4f42-8225-408279c3474b"), 13, 3 },
                    { 62, new Guid("8c9516eb-94b1-47db-a248-786c7c56fb24"), 13, 5 },
                    { 63, new Guid("74c2f307-52b7-47b2-b211-c1e0a552ee0b"), 13, 4 },
                    { 64, new Guid("4d480ce4-35c1-491c-aa00-1c6cf7cb5b44"), 13, 5 },
                    { 65, new Guid("ea5ac5c3-d8df-421e-b4b8-9946a4b2c5d7"), 13, 3 },
                    { 66, new Guid("ed2778b5-a4df-4a15-ba2e-8d6ee89246dd"), 14, 1 },
                    { 67, new Guid("c61ff318-4ba0-4c92-bdc7-b3dd54a18bbb"), 14, 2 },
                    { 68, new Guid("375e8a26-31a9-4ae8-9d64-12b9b747a434"), 14, 3 },
                    { 69, new Guid("cda42e83-b0a9-456a-a585-d94d6991e1f5"), 14, 4 },
                    { 70, new Guid("fe662b13-f40e-4b6a-9bcf-7d7be626ef33"), 14, 3 },
                    { 71, new Guid("6676ffb6-a3ee-4e71-a6da-befeeaaa9457"), 15, 5 },
                    { 72, new Guid("6f5decf0-b982-43dd-ad08-00a9e7a30da7"), 15, 4 },
                    { 73, new Guid("eb821d27-3f2f-48ec-8f35-3b189d7d05a1"), 15, 5 },
                    { 74, new Guid("8e69d0fe-83b5-4ae2-bfab-e90919285136"), 15, 2 },
                    { 75, new Guid("90b22ed6-eecc-4941-8624-73ef76a29afa"), 15, 2 },
                    { 76, new Guid("08d294e5-5d0f-4cb5-9c2d-5b5a38eea673"), 16, 2 },
                    { 77, new Guid("3395dfcf-78a7-4a1c-8aed-59558e8ba416"), 16, 2 },
                    { 78, new Guid("eafa33de-ab84-47e0-8c08-4a8f9aedf8fe"), 16, 2 },
                    { 79, new Guid("5ce7292a-60ef-4563-8000-cf064b85444c"), 16, 2 },
                    { 80, new Guid("d25db75e-6f3e-4c29-922c-885ac893506b"), 16, 3 },
                    { 81, new Guid("9d39eb9c-6c78-42c5-b319-c30ba240ecf8"), 17, 1 },
                    { 82, new Guid("773ede3e-a071-4803-8c91-84fe73f16918"), 17, 3 },
                    { 83, new Guid("894330af-9727-427c-bcaf-0843d3d03f95"), 17, 4 },
                    { 84, new Guid("7effcb5e-12e0-455e-bd2a-cef4adfcc320"), 17, 3 },
                    { 85, new Guid("6626a751-9253-4c3f-99ac-35f6f96dc55f"), 17, 4 },
                    { 86, new Guid("54a07769-78ab-4f2e-9a15-0bd1497a0500"), 18, 3 },
                    { 87, new Guid("fdd4b6c4-0115-4ea7-828d-20ef703c1581"), 18, 1 },
                    { 88, new Guid("0593c203-66e0-44e6-8f72-9104c1e3e811"), 18, 4 },
                    { 89, new Guid("023f270c-4512-4cd1-89db-d6f8dd5e3ffc"), 18, 3 },
                    { 90, new Guid("61f76e50-2b31-4c1b-b6d4-7268167dcf39"), 18, 3 },
                    { 91, new Guid("23973e29-024f-4629-9ea8-920feb7a739d"), 19, 2 },
                    { 92, new Guid("4439cbed-2593-4a13-b91a-79000b581f8f"), 19, 1 },
                    { 93, new Guid("3419343c-8969-41a7-b114-95e31a62e273"), 19, 1 },
                    { 94, new Guid("f295fe0e-f266-47c4-82d7-7b77826d71a5"), 19, 3 },
                    { 95, new Guid("a36ffb27-7034-4f84-bed3-54b49c533440"), 19, 5 },
                    { 96, new Guid("4e594e29-5e47-4024-9ad4-1d17ef287f1f"), 20, 3 },
                    { 97, new Guid("1f5c0704-a65d-4fad-94cc-ba9f8a85c3fe"), 20, 4 },
                    { 98, new Guid("6941fad9-63bb-4f0b-a60d-680719153580"), 20, 3 },
                    { 99, new Guid("4c74395c-3ecc-4615-95a4-f16c505b9b28"), 20, 1 },
                    { 100, new Guid("a49a91f1-e2e2-4b7e-a052-a61009b6943b"), 20, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlbumImage_ImageId",
                table: "AlbumImage",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Albums_UserId",
                table: "Albums",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_UserId",
                table: "Images",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ImageId",
                table: "Posts",
                column: "ImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_StatusId",
                table: "Posts",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_PostId",
                table: "Reactions",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_UserId",
                table: "Reactions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlbumImage");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Reactions");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
