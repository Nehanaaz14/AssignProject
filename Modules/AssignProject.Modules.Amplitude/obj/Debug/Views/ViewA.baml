   M S B A M L   `   `   `  ����Z  VAssignProject.Modules.Amplitude, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:AssignProject.Modules.Amplitude.Views%AssignProject.Modules.Amplitude.Views  5      g8clr-namespace:AssignProject.Modules.Amplitude.ViewModels*AssignProject.Modules.Amplitude.ViewModels  5      1    +AssignProject.Modules.Amplitude.Views.ViewA   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Elocal3clr-namespace:AssignProject.Modules.Amplitude.Views     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      O
viewmodels8clr-namespace:AssignProject.Modules.Amplitude.ViewModels     5      Z VPrism.DryIoc.Wpf, Version=8.0.0.1909, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59T PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5
      $	��600��5	      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel$
  TrueX�5      
�������.��5      �  � ColumnDefinitions 5   
   �� 5      $��20 �6   �� 5      $
��Auto �6   �� 5      $��20 �6    � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      $��10 �6   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5       $	��175 �6   �� 5!      $��30 �6   �� 5#      $
��Auto �6   �� 5$      $��10 �6   �� 5&      $
��Auto �6   �� 5'      $��20 �6   �� 5)      $��65 �6   �� 5*      $��30 �6   .+�5,   
   �� $��14��5-      $��3��5.      �� 5/   
   $��0��50      $��1��51      $��25,0,433.6,10.2q�52      $��Bottom=�53      $��	AMPLITUDE��54      $
��Bold"�6   $��2��60   �� 55   
   $��0��56      $��2��57      $��0,29,16.6,28.8q�58      $��Right=�59      $��Center=�5:      ��5;      �� *CloseCommand+�� 5<   
   $��2��5=      $��1��5>      $��25,0,433.6,0q�5?      $��Bottom=�5@       �� Orientation$ 
Horizontal=�5A      .+�5B      �� -   5         ��Name$ CurrentAmplitudeLbl��5C      $��Active Amplitude:��5D      $
��Bold"�5E      �� 5F      -   5        $ CurrentAmplitudeTxt��5G      $��6,0,0,0q�5H      ��5I      �� *CurrentAmplitude+5K      �� 5L   
   $��4��5M      $��1��5N      $��25,0,0,0q�5O      $��Bottom=�5P      $ 
Horizontal=�5Q      .+�5R      �� -   5        $ TargetAmplitudeLbl��5S      $��Target Amplitude:��5T      $
��Bold"�5U      �� 5V      -   5        $ TargetAmplitudeValue��5W      $��6,0,0,0q�5X      ��5Y      ��  �� ElementName$ AmplitudeListBox�� �� Path$! SelectedItem.AmplitudeValue�5Z      �� 5\   
   -   5         ��Name$ AmplitudeListBox��5\      $��6��5]      $��1��5^      $
��Auto��5_         ListBoxItemsPanelTemplate#��[  5`      $��Hidden=�5a      $��Hidden=�5b       �� SelectedIndex 5c      �� *SelectedAmplitudeItemIndex+��5d      �� 5e      .R�5f      �� 	 ��Name$	 ampstborder��6   $��50��63   $��40��6A   $��1q�6P   
 �� CornerRadius$
 5��6d   $��
20,10,0,10q�6u   ._�5g      �� $ amplitudeValue��6$   $	��100��6<   $��Center=�6H   $��Center=�6e    �� TextAlignment$ Center=�6�   ��6�   �� $ AmplitudeValue�6�   5h      5i      5k      �� 5p   
   $��8��5q      $��1��5r      $ Vertical=�5s      .+�5t      �� -   5        $ RampSpeedLbl��5u      $��26,0,0,5q�5v      $
��Bold"�5w      $��
RAMP SPEED��5x      �� 5y      -   5         ��Name$ RampSpeedCbx��5z      $	��180��5{      $��30��5|      $��26,0,0,0q�5}      $
��Left=�5~       �� DisplayMemberPath$
 Name��5      $��14(�5�      ��5�      �� *RampSpeedItemList+ �� SelectedItem 5�      �� *SelectedRampSpeed+5�      �� 5�   
   $��10��5�      $��1��5�      $��25,0,0,0q�5�      $
��Left=�5�      $ 
Horizontal=�5�      .+�5�      �� $
��Auto��5�      $��28��5�      $��1,0,0,0q�5�      $	��Top=�5�      $��.��5�      $��24(�5�      
���   �5�      �� 5�      $��6��6   
������ 6"   �� 5�      -   5        $ RampMessageText��5�      $��28��5�      $	��640��5�      $��Center=�5�      $��14(�5�      $
��Bold"�5�      ��5�      �� *MessageText+5�      �� 5�   
   -	   5         ��Name$ AcceptButton��5�      $��12��5�      $��1��5�      $	��180��5�      $��40��5�      $��Accept��5�       �� 	IsDefault . $��40,5.6,493.6,5q�5�      ��5�      �� *	OkCommand+5�      5�      5�      