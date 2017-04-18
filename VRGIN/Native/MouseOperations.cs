using UnityEngine;
using static VRGIN.Native.WindowsInterop;

namespace VRGIN.Native
{
    public class MouseOperations
    {
        public static Resolution gameRes = Screen.currentResolution;
        public static float gameRatio = (float)gameRes.width / gameRes.height;

        public static RECT clientRect = WindowManager.GetClientRect();
        public static float clientWidth = clientRect.Right - clientRect.Left;
        public static float clientHeight = clientRect.Bottom - clientRect.Top;
        public static float  windowRatio = (float)clientWidth / clientHeight;

        public static bool isWindowWider = gameRatio < windowRatio;

        public static float widthProjectionMultiplier = (float)clientHeight / gameRes.height;
        public static float widthProjection = widthProjectionMultiplier * gameRes.width;
        public static int leftIdent = (int)((clientWidth - widthProjection) / 2);

        public static float heightProjectionMultiplier = (float)clientWidth / gameRes.width;
        public static float heightProjection = heightProjectionMultiplier * gameRes.height;
        public static int topIdent = (int)((clientHeight - heightProjection) / 2);

        public static void SetCursorPosition(int X, int Y)
        {
            SetCursorPos(X, Y);
        }

        public static void SetClientCursorPosition(int x, int y)
        {
            var clientRect = WindowManager.GetClientRect();
            SetCursorPos(x + clientRect.Left, y + clientRect.Top);
        }

        public static void SetClientCursorPositionFullscreen(int x, int y)
        {
            if (isWindowWider)
            {
                SetCursorPos((int)(x * widthProjectionMultiplier) + leftIdent, (int)(y * widthProjectionMultiplier));
            } else
            {
                SetCursorPos((int)(x * heightProjectionMultiplier), (int)(y * heightProjectionMultiplier) + topIdent);
            }
        }



        public static POINT GetClientCursorPosition()
        {
            var pos = GetCursorPosition();
            var clientRect = WindowManager.GetClientRect();

            return new POINT(pos.X - clientRect.Left, pos.Y - clientRect.Top);
        }

        public static void SetCursorPosition(POINT point)
        {
            SetCursorPos(point.X, point.Y);
        }

        public static POINT GetCursorPosition()
        {
            POINT currentMousePoint;
            var gotPoint = GetCursorPos(out currentMousePoint);
            if (!gotPoint) { currentMousePoint = new POINT(0, 0); }
            return currentMousePoint;
        }


        public static void MouseEvent(MouseEventFlags value)
        {
            POINT position = GetCursorPosition();

            mouse_event
                ((int)value,
                 position.X,
                 position.Y,
                 0,
                 0)
                ;
        }

    }
}