   M S B A M L   `   `   `  ����Z  VAssignProject.Modules.Amplitude, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:AssignProject.Modules.Amplitude.Views%AssignProject.Modules.Amplitude.Views  5      7    1AssignProject.Modules.Amplitude.Views.RateSetting   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Elocal3clr-namespace:AssignProject.Modules.Amplitude.Views     5      Z VPrism.DryIoc.Wpf, Version=8.0.0.1909, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59T PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel$
  TrueX�5      $	��125��5
      $	��250��6   .��5      � $��2q�6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��2* �6    � ColumnDefinitions 5   
   �� 5      $	��35* �6   �� 5      $	��65* �6   .+�5   
   �� -   5         ��Name$ 	ratingLbl��5      $��RATING��6#   $
��Left=�64   $	��Top=�6P   $��68��6h   $��8,12,0,0q�6s   �� 5   
   -   5        $ scoreLbl��5      $��1��6"   $��SCORE��6/   $
��Left=�6?   $��8,1,0,0q�6Z   $	��Top=�6k   $��78��6�   $��2��6�   �� 5   
   -   5        $ leastLbl��5      $��1��6"   $��LEAST��62   $
��Left=�6B   $	��Top=�6^   $��78��6v   $��
0.4,13,0,0q�6�   �� 5   
   -   5        $ bestLbl��5      $��1��6!   $
��BEST��61   $
��Left=�6@   $	��Top=�6[   $��78��6s    J� RenderTransformOrigin$ 1.852,0.431&�6~   $��80.4,13,0,0q�6�   $��Right=�6�   �� 5   
   $��1��6   $��1��5      $	��1,0q�5      .+�5      A�  A� Columns$ 5��6   .+�5      �� -   5         ��Name$ 
ratingFour��5      $��4��6-   ��69   �� *	setFour+�� 5      -   5        $ ratingThree��5      $��3��6.   ��6:   �� *
setThree+�� 5      -   5        $ 	ratingTwo��5      $��2��6,   ��68   �� *setTwo+�� 5      -   5        $ 	ratingOne��5      $��1��6,   ��68   �� *setOne+�� 5       -	   5        $ 
ratingZero��5       $��0��6-   ��69   �� *	setZero+5!      5"      �� 5#   
   -
   5        $ 
BtnConfirm��5#   .   $��1��6   $��1��6   $��Confirm��6B   $��17.4,40,90.6,23.8q�6T   $ 0.019,0.668&�6o   ��6�   �� *ConfirmButtonCommand+5$      5%      