   M S B A M L   `   `   `  ����Z  VAssignProject.Modules.Amplitude, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:AssignProject.Modules.Amplitude.Views%AssignProject.Modules.Amplitude.Views  5      ?    9AssignProject.Modules.Amplitude.Views.ApplicationSettings   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Elocal3clr-namespace:AssignProject.Modules.Amplitude.Views     5      Z VPrism.DryIoc.Wpf, Version=8.0.0.1909, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59T PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel$
  TrueX�5      .��5      � $��15q�6   
������� � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��4* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��2* �6   .+�5   
   �� $ ��Change Language Preference��6   $
��Left=�5      $��2��5      $��Bottom=�5      
������ $��0q�5      $��2q�5      $��5,5,5,0q�5      �� 5   
   $��1��6   $��2��5      $��Clear Database Data��5       $
��Left=�5!      $��Bottom=�5"      
������ $��2q�5$      $��0q�5%      $��5,5,5,0q�5&      ��6"   �� *ClearDatabaseCommand+�� 5'   
   $��2��6   $��Run Automation��6   $��2��5(      $
��Left=�5)      $��0q�5*      $��Bottom=�5+      
������ $��2q�5-      $��5,5,5,0q�5.      �� 5/   
   $��	Lead Type��6   $
��Left=�6$   $��	10,27,0,0q�6?   $��3��6R   $	��Top=�6_   $��74��6w    J� RenderTransformOrigin$ 0.534,0.244&�6�   �� 50   
   $
��Port��6   $
��Left=�6   $��	10,62,0,0q�6:   $��3��6M   $	��Top=�6Z   $��74��6r   �� 51   
   $��
Hemisphere��6   $
��Left=�6%   $��	10,92,0,0q�6@   $��3��6S   $	��Top=�6`   $��74��6x   �� 52   
   $��1��6   $
��Left=�6#   $��22.4,27,0,0q�6>   $��3��6S   $	��Top=�6`   $	��120��6x    �� DisplayMemberPath$
 Lead��53      ��52   �   �� *
LeadType+ �� SelectedItem 6�   �� *SelectedLead+53      �� 54   
   $��1��6   $
��Left=�6#   $��22.4,65,0,0q�6>   $��3��6S   $	��Top=�6`   $	��120��6x   $
 Port��55      ��54   �   �� *
PortType+ 6�   �� *SelectedPort+55      �� 56   
   $��1��6   $
��Left=�6#   $��22.4,95,0,0q�6>   $��3��6S   $	��Top=�6`   $	��120��6x   $ 
Hemisphere��6�   ��6�   �� *Hemispheres+ 6�   �� *SelectedHemisphere+6�   �� 57   
   $��1��6   $
��Left=�6#   $	��121��6>   $��10.4,18,0,0q�6K   $��3��6`   $	��Top=�6m   $	��145��6�   59      �� 5:   
   -   5         ��Name$ backBtn��5:      $
��Back��6"   $��3��61   $��45��6>   
������ $��0q�5;      $��10,132.8,34.6,0.6q�6E   ��6%   �� *BackCommand+6E   5<      5=      