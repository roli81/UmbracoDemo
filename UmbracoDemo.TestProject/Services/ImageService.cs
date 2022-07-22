using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;
using UmbracoDemo.TestProject.Models.Echarts;

namespace UmbracoDemo.TestProject.Services
{


    public interface IImageService
    {
        Image GetImage(
            IPublishedContent node,
            int? width = null,
            int? height = null,
            ImageCropMode imageCropMode = ImageCropMode.Crop
            );

        IEnumerable<Image> GetImages(
            IEnumerable<IPublishedContent>
            nodes,
            int? width = null,
            int? height = null,
            ImageCropMode imageCropMode = ImageCropMode.Crop
            );
    }


    public class ImageService : IImageService
    {
        public Image GetImage(IPublishedContent node, int? width = null, int? height = null, ImageCropMode imageCropMode = ImageCropMode.Crop)
        {
            if (node == null) return null;

            return new Image()
            {
                Alt = node.Name,
                Src = node.GetCropUrl(width, height, imageCropMode: imageCropMode, urlMode: UrlMode.Absolute)
            };
        }

        public IEnumerable<Image> GetImages(IEnumerable<IPublishedContent> nodes, int? width = null, int? height = null, ImageCropMode imageCropMode = ImageCropMode.Crop)
        {
            if (nodes == null) return null;

            return nodes.Select(n => new Image()
            {
                Alt = n.Name,
                Src = n.GetCropUrl(width, height, imageCropMode: imageCropMode)
            });
        }
    }
}
