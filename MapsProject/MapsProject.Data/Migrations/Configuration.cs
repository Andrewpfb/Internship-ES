using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using MapsProject.Data.Models;

namespace MapsProject.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<EF.MapObjectContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MapsProject.Data.EF.MapObjectContext";
        }

        protected override void Seed(EF.MapObjectContext context)
        {
            Role adminRole = new Role { Id = 1, Name = "Admin" };
            User adminUser = new User { Id = 1, Name = "Admin", Password = "1156372900", Role = adminRole, RoleId = 1 };
            // password - "Admin".getHashCode().ToString();

            MapObject mapObject1 = new MapObject
            {
                Id = 1,
                ObjectName = "Food",
                GeoLat = 53.888103355788509,
                GeoLong = 27.537167072296143,
                Status = MapsProject.Models.Enums.Status.Approved,
                IsDelete = false,
                Tags = new List<Tag>()
            };
            MapObject mapObject2 = new MapObject
            {
                Id = 2,
                ObjectName = "Cantine",
                GeoLong = 27.547123432159424,
                GeoLat = 53.888685085306754,
                Status = MapsProject.Models.Enums.Status.NeedModerate,
                IsDelete = true,
                Tags = new List<Tag>()
            };
            MapObject mapObject3 = new MapObject
            {
                Id = 3,
                ObjectName = "Angular",
                GeoLong = 27.532510757446289,
                GeoLat = 53.891998259925408,
                Status = MapsProject.Models.Enums.Status.Approved,
                IsDelete = false,
                Tags = new List<Tag>()
            };
            MapObject mapObject4 = new MapObject
            {
                Id = 4,
                ObjectName = "Bank",
                GeoLong = 27.546651363372803,
                GeoLat = 53.886256943254722,
                Status = MapsProject.Models.Enums.Status.Approved,
                IsDelete = false,
                Tags = new List<Tag>()
            };

            Tag tag1 = new Tag
            {
                Id = 1,
                TagName = "Food",
                IsDelete = false,
                MapObjects = new List<MapObject> { mapObject1, mapObject2 }
            };
            Tag tag2 = new Tag
            {
                Id = 2,
                TagName = "Cantine",
                IsDelete = false,
                MapObjects = new List<MapObject> { mapObject2 }
            };
            Tag tag3 = new Tag
            {
                Id = 3,
                TagName = "Angular",
                IsDelete = false,
                MapObjects = new List<MapObject> { mapObject3 }
            };
            Tag tag4 = new Tag
            {
                Id = 4,
                TagName = "Bank",
                IsDelete = false,
                MapObjects = new List<MapObject> { mapObject4 }
            };

            mapObject1.Tags = new List<Tag> { tag1 };
            mapObject2.Tags = new List<Tag> { tag1, tag2 };
            mapObject3.Tags = new List<Tag> { tag3 };
            mapObject4.Tags = new List<Tag> { tag4 };

            context.Roles.Add(adminRole);
            context.Users.Add(adminUser);

            context.Tags.Add(tag1);
            context.Tags.Add(tag2);
            context.Tags.Add(tag3);
            context.Tags.Add(tag4);

            context.MapsObjects.Add(mapObject1);
            context.MapsObjects.Add(mapObject2);
            context.MapsObjects.Add(mapObject3);
            context.MapsObjects.Add(mapObject4);

            context.SaveChanges();
        }
    }
}
