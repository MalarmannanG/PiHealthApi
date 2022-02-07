using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PiHealth.Migrations
{
    public partial class Initail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessModule",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleCode = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessModule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 35, nullable: true),
                    PhoneNo = table.Column<string>(maxLength: 20, nullable: true),
                    UserType = table.Column<string>(maxLength: 20, nullable: false),
                    Gender = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    SerialNumber = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    LastLoggedIn = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AuditLog",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(maxLength: 50, nullable: true),
                    Controller = table.Column<string>(maxLength: 50, nullable: false),
                    Action = table.Column<string>(maxLength: 50, nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    IP = table.Column<string>(maxLength: 50, nullable: true),
                    UserAgent = table.Column<string>(maxLength: 200, nullable: true),
                    Value1 = table.Column<string>(maxLength: 5000, nullable: true),
                    Value2 = table.Column<string>(maxLength: 5000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentMaster",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoctorMaster",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Qualification = table.Column<string>(nullable: true),
                    ClinicName = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    Department = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    TelNo = table.Column<string>(nullable: true),
                    PhoneNo1 = table.Column<string>(nullable: true),
                    PhoneNo2 = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Percentage = table.Column<double>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrescriptionMaster",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenericName = table.Column<string>(nullable: true),
                    CategoryName = table.Column<string>(nullable: true),
                    MedicinName = table.Column<string>(nullable: true),
                    Strength = table.Column<string>(nullable: true),
                    Units = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TemplateMaster",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Compliants = table.Column<string>(nullable: true),
                    Examination = table.Column<string>(nullable: true),
                    Impression = table.Column<string>(nullable: true),
                    Advice = table.Column<string>(nullable: true),
                    Plan = table.Column<string>(nullable: true),
                    FollowUp = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestMaster",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Department = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccessFunction",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FuctionCode = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    ModuleID = table.Column<long>(nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessFunction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessFunction_AccessModule_ModuleID",
                        column: x => x.ModuleID,
                        principalTable: "AccessModule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccessTokenHash = table.Column<string>(nullable: true),
                    IDAccessTokenExpiresDateTime = table.Column<DateTimeOffset>(nullable: false),
                    RefreshTokenIdHash = table.Column<string>(nullable: true),
                    RefreshTokenExpiresDateTime = table.Column<DateTimeOffset>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToken_AppUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientName = table.Column<string>(nullable: false),
                    Initial = table.Column<string>(nullable: true),
                    HrNo = table.Column<string>(nullable: true),
                    AttendarName = table.Column<string>(nullable: true),
                    PatientGender = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MobileNumber = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: true),
                    Age = table.Column<float>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    DoctorMasterId = table.Column<long>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true),
                    UlID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patient_DoctorMaster_DoctorMasterId",
                        column: x => x.DoctorMasterId,
                        principalTable: "DoctorMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TemplatePrescription",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateMasterId = table.Column<long>(nullable: false),
                    MedicinName = table.Column<string>(nullable: true),
                    Strength = table.Column<string>(nullable: true),
                    BeforeFood = table.Column<bool>(nullable: false),
                    Morning = table.Column<bool>(nullable: false),
                    Noon = table.Column<bool>(nullable: false),
                    Night = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    NoOfDays = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplatePrescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplatePrescription_TemplateMaster_TemplateMasterId",
                        column: x => x.TemplateMasterId,
                        principalTable: "TemplateMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccessRoleFunction",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    FunctionID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessRoleFunction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessRoleFunction_AccessFunction_FunctionID",
                        column: x => x.FunctionID,
                        principalTable: "AccessFunction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<long>(nullable: true),
                    AppUserId = table.Column<long>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    VisitType = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    DayOrNight = table.Column<string>(nullable: true),
                    TimeOfAppintment = table.Column<string>(nullable: true),
                    AppointmentDateTime = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointment_AppUser_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointment_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientFiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentID = table.Column<long>(nullable: true),
                    PatientID = table.Column<long>(nullable: true),
                    FilePath = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientFiles_Appointment_AppointmentID",
                        column: x => x.AppointmentID,
                        principalTable: "Appointment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PatientFiles_Patient_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientProfile",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<long>(nullable: false),
                    DoctorId = table.Column<long>(nullable: false),
                    TemplateMasterId = table.Column<long>(nullable: true),
                    AppointmentId = table.Column<long>(nullable: false),
                    Compliants = table.Column<string>(nullable: true),
                    Examination = table.Column<string>(nullable: true),
                    Impression = table.Column<string>(nullable: true),
                    Advice = table.Column<string>(nullable: true),
                    Plan = table.Column<string>(nullable: true),
                    FollowUp = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientProfile_Appointment_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientProfile_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientProfile_TemplateMaster_TemplateMasterId",
                        column: x => x.TemplateMasterId,
                        principalTable: "TemplateMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VitalsReport",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentID = table.Column<long>(nullable: true),
                    PatientID = table.Column<long>(nullable: true),
                    Height = table.Column<double>(nullable: false),
                    Weight = table.Column<double>(nullable: false),
                    BloodPresure = table.Column<string>(nullable: true),
                    BP = table.Column<double>(nullable: false),
                    Pulse = table.Column<double>(nullable: false),
                    Temprature = table.Column<double>(nullable: false),
                    SpO2 = table.Column<double>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VitalsReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VitalsReport_Appointment_AppointmentID",
                        column: x => x.AppointmentID,
                        principalTable: "Appointment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VitalsReport_Patient_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiagnosisMaster",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PatientProfileId = table.Column<long>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosisMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiagnosisMaster_PatientProfile_PatientProfileId",
                        column: x => x.PatientProfileId,
                        principalTable: "PatientProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientTest",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientProfileId = table.Column<long>(nullable: false),
                    TestMasterId = table.Column<long>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientTest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientTest_PatientProfile_PatientProfileId",
                        column: x => x.PatientProfileId,
                        principalTable: "PatientProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientTest_TestMaster_TestMasterId",
                        column: x => x.TestMasterId,
                        principalTable: "TestMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prescription",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientProfileId = table.Column<long>(nullable: false),
                    MedicinName = table.Column<string>(nullable: true),
                    GenericName = table.Column<string>(nullable: true),
                    Strength = table.Column<string>(nullable: true),
                    BeforeFood = table.Column<bool>(nullable: false),
                    Morning = table.Column<bool>(nullable: false),
                    Noon = table.Column<bool>(nullable: false),
                    Night = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    NoOfDays = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescription_PatientProfile_PatientProfileId",
                        column: x => x.PatientProfileId,
                        principalTable: "PatientProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientDiagnosis",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientProfileId = table.Column<long>(nullable: false),
                    DiagnosisMasterId = table.Column<long>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientDiagnosis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientDiagnosis_DiagnosisMaster_DiagnosisMasterId",
                        column: x => x.DiagnosisMasterId,
                        principalTable: "DiagnosisMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientDiagnosis_PatientProfile_PatientProfileId",
                        column: x => x.PatientProfileId,
                        principalTable: "PatientProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessFunction_ModuleID",
                table: "AccessFunction",
                column: "ModuleID");

            migrationBuilder.CreateIndex(
                name: "IX_AccessRoleFunction_FunctionID",
                table: "AccessRoleFunction",
                column: "FunctionID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_AppUserId",
                table: "Appointment",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_PatientId",
                table: "Appointment",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosisMaster_PatientProfileId",
                table: "DiagnosisMaster",
                column: "PatientProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_DoctorMasterId",
                table: "Patient",
                column: "DoctorMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientDiagnosis_DiagnosisMasterId",
                table: "PatientDiagnosis",
                column: "DiagnosisMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientDiagnosis_PatientProfileId",
                table: "PatientDiagnosis",
                column: "PatientProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientFiles_AppointmentID",
                table: "PatientFiles",
                column: "AppointmentID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientFiles_PatientID",
                table: "PatientFiles",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientProfile_AppointmentId",
                table: "PatientProfile",
                column: "AppointmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientProfile_PatientId",
                table: "PatientProfile",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientProfile_TemplateMasterId",
                table: "PatientProfile",
                column: "TemplateMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientTest_PatientProfileId",
                table: "PatientTest",
                column: "PatientProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientTest_TestMasterId",
                table: "PatientTest",
                column: "TestMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_PatientProfileId",
                table: "Prescription",
                column: "PatientProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplatePrescription_TemplateMasterId",
                table: "TemplatePrescription",
                column: "TemplateMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToken_UserId",
                table: "UserToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VitalsReport_AppointmentID",
                table: "VitalsReport",
                column: "AppointmentID",
                unique: true,
                filter: "[AppointmentID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_VitalsReport_PatientID",
                table: "VitalsReport",
                column: "PatientID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessRoleFunction");

            migrationBuilder.DropTable(
                name: "AuditLog");

            migrationBuilder.DropTable(
                name: "DepartmentMaster");

            migrationBuilder.DropTable(
                name: "PatientDiagnosis");

            migrationBuilder.DropTable(
                name: "PatientFiles");

            migrationBuilder.DropTable(
                name: "PatientTest");

            migrationBuilder.DropTable(
                name: "Prescription");

            migrationBuilder.DropTable(
                name: "PrescriptionMaster");

            migrationBuilder.DropTable(
                name: "TemplatePrescription");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "VitalsReport");

            migrationBuilder.DropTable(
                name: "AccessFunction");

            migrationBuilder.DropTable(
                name: "DiagnosisMaster");

            migrationBuilder.DropTable(
                name: "TestMaster");

            migrationBuilder.DropTable(
                name: "AccessModule");

            migrationBuilder.DropTable(
                name: "PatientProfile");

            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "TemplateMaster");

            migrationBuilder.DropTable(
                name: "AppUser");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "DoctorMaster");
        }
    }
}
