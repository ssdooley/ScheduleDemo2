using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Schedule.Data;

namespace Schedule.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20170621133359_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Schedule.Data.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Schedule.Data.Models.Commitment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<int>("CategoryId");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Location");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Subject");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Commitments");
                });

            modelBuilder.Entity("Schedule.Data.Models.CommitmentPerson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CommitmentId");

                    b.Property<int>("PersonId");

                    b.HasKey("Id");

                    b.HasIndex("CommitmentId");

                    b.HasIndex("PersonId");

                    b.ToTable("CommitmentPeople");
                });

            modelBuilder.Entity("Schedule.Data.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("People");
                });

            modelBuilder.Entity("Schedule.Data.Models.Commitment", b =>
                {
                    b.HasOne("Schedule.Data.Models.Category", "Category")
                        .WithMany("Commitments")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Schedule.Data.Models.CommitmentPerson", b =>
                {
                    b.HasOne("Schedule.Data.Models.Commitment", "Commitment")
                        .WithMany("CommitmentPeople")
                        .HasForeignKey("CommitmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Schedule.Data.Models.Person", "Person")
                        .WithMany("CommitmentPeople")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
