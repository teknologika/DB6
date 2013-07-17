// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace DB6Demo
{
	[Register ("DB6DemoViewController")]
	partial class DB6DemoViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton Button { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel Label { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (Button != null) {
				Button.Dispose ();
				Button = null;
			}

			if (Label != null) {
				Label.Dispose ();
				Label = null;
			}
		}
	}
}
