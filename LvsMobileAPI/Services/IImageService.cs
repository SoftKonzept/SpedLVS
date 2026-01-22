using Common.ApiModels;
using LVS.ViewData;
using LvsMobileAPI.DataConnection;

namespace LvsMobileAPI.Services
{
    public interface IImageService
    {
        public ResponseImage PUT_Image_Add(ResponseImage response);
        public ResponseImage POST_Image_Add(ResponseImage response);

    }

    public class ImageService : IImageService
    {
        private SvrSettings srv;
        public ImageService()
        {
            srv = new SvrSettings();
        }

        public ResponseImage PUT_Image_Add(ResponseImage response)
        {
            srv = new SvrSettings();
            response.Success = false;
            ImageViewData viewData = new ImageViewData(response.UserId, response.Image);
            try
            {
                viewData.Add();
                if (response.Image.Created > new DateTime(1900, 1, 1))
                {
                    viewData.FillByCreated();
                }
                response.Success = (viewData.Image.Id > 0);
                if (response.Success)
                {
                    viewData.Fill();
                    response.Image = viewData.Image.Copy();
                    response.Info = "Der Datensatz wurde erfolgreich eingefügt!";
                }
                else
                {
                    response.Error = "Der Datensatz konnte nicht eingefügt werden!";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = ex.Message;
            }
            return response;
        }

        /// <summary>
        ///             POST finished StoreOut 
        /// </summary>
        public ResponseImage POST_Image_Add(ResponseImage response)
        {
            srv = new SvrSettings();
            response.Success = false;
            ImageViewData viewData = new ImageViewData(response.UserId, response.Image);
            try
            {
                viewData.Add();
                response.Success = (viewData.Image.Id > 0);
                if (response.Success)
                {
                    //viewData.Fill();
                    response.Image = null;
                    response.Info = "Der Datensatz wurde erfolgreich eingefügt!";
                }
                else
                {
                    response.Error = "Der Datensatz konnte nicht eingefügt werden!";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = ex.Message;
            }
            return response;
        }
    }
}
