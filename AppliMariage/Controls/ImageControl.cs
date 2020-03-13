using AppliMariage.Selectors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace AppliMariage.Controls
{
    [TemplatePart(Name = "PART_ContentHolder", Type = typeof(Image))]
    [TemplatePart(Name = "PART_AnimateBottom", Type = typeof(Storyboard))]
    [TemplatePart(Name = "PART_AnimateTop", Type = typeof(Storyboard))]
    public class ImageControl : Control
    {
        private ContentControl _contentHolder;
        private Storyboard _animateBottom;
        private Storyboard _animateTop;

        static ImageControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageControl), new FrameworkPropertyMetadata(typeof(ImageControl)));
        }
       
        public string Content
        {
            get { return (string)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(string), typeof(ImageControl), new PropertyMetadata(null, ContentPropertyChanged));

        private static void ContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Refresh(d);
        }


        public Color Couleur
        {
            get { return (Color)GetValue(CouleurProperty); }
            set { SetValue(CouleurProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CouleurProperty =
            DependencyProperty.Register("Couleur", typeof(Color), typeof(ImageControl), new PropertyMetadata(Colors.Black, CouleurPropertyChanged));

        private static void CouleurPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Refresh(d);
        }

        public static void Refresh(object tempcontrol) {
            var control = tempcontrol as ImageControl;

            if (control == null
                || control._animateBottom == null || control._animateTop == null 
                || control._contentHolder == null) 
                return;


            Brush brush = new SolidColorBrush(control.Couleur);

            if (control._contentHolder.Content == null)
            {
                control._contentHolder.Content = control.Content;
                control._contentHolder.Foreground = brush;
                return;
            }
            
            control._animateBottom.Completed += (o, evt) =>
            {
                control._contentHolder.Content = control.Content;
                control._contentHolder.Foreground = brush;
                control._animateTop.Begin();
            };    
            control._animateBottom.Begin();
            
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _contentHolder = GetTemplateChild("PART_ContentHolder") as ContentControl;
            _contentHolder.Content = Content;
            _contentHolder.Foreground = new SolidColorBrush(Couleur);
            _animateBottom = GetTemplateChild("PART_AnimateBottom") as Storyboard;
            _animateTop = GetTemplateChild("PART_AnimateTop") as Storyboard;
            Refresh(this);
        }


    }
}
