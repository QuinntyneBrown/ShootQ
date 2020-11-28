using BuildingBlocks.Abstractions;
using ShootQ.Core.DomainEvents;
using System;
using System.Collections.Generic;

namespace ShootQ.Core.Models
{
    public class PhotoGallery : AggregateRoot
    {
        public PhotoGallery(string name)
        {
            Apply(new PhotoGalleryCreated(name));
        }
        protected override void When(dynamic @event) => When(@event);

        public void When(PhotoGalleryCreated photoGalleryCreated)
        {
            Name = photoGalleryCreated.Name;
            Photos = new HashSet<PhotoGallery.Photo>();
        }

        protected override void EnsureValidState()
        {

        }

        public Guid PhotoGalleryId { get; private set; }
        public string Name { get; set; }
        public ICollection<PhotoGallery.Photo> Photos { get; set; }

        public record Photo
        {
            public Guid DigitalAssetId { get; set; }
            public string Name { get; set; }
            public DateTime Created { get; set; }
        }
    }
}
