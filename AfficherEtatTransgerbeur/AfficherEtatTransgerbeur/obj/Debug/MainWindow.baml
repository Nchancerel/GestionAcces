   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=1.4.1.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   S OAfficherEtatTransgerbeur, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:AfficherEtatTransgerbeurAfficherEtatTransgerbeur 5	   	   )   #AfficherEtatTransgerbeur.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   J FWpfAnimatedGif, Version=1.4.14.0, Culture=neutral, PublicKeyToken=null,gif"http://wpfanimatedgif.codeplex.com  5   	   Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5   	   JControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro  5   	   2local&clr-namespace:AfficherEtatTransgerbeur  5	   	   "  WpfAnimatedGif.ImageBehavior    AnimateInDesignMode  .  � Title$ 
MainWindow��5   	    � WindowStyle$
 None=�5   	    � WindowState$ 	Maximized=�5   	    � 
ResizeMode$ NoResize=�5   	   $	��786��5      $
��1366��6    � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $I Cpack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml?�6%   �� 5      $F @pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml?�6%   �� 5      $G Apack://application:,,,/MahApps.Metro;component/Styles/Colors.xaml?�6%   �� 5      $M Gpack://application:,,,/MahApps.Metro;component/Styles/Accents/Blue.xaml?�6%   �� 5      $R Lpack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseLight.xaml?�6%   %    5      .��5      � }�. $��0��6   |�. ���	#FFF1F1F1 � RowDefinitions 5   
   �� 5       $��0* �6   �� 5!      $	��47* �6   �� 5"      $	��246 �6   �� 5#      $	��88* �6   	 � ColumnDefinitions	 5&   
   �� 5'      $
��102* �6   .+�5+   
   �� -   5        
 ��Name$
 LogoTransgerbeur��5+      $��img/Logobannierelong.png��6*   $
��Left=�6L   $	��122��6g   $��2��6t   $	��Top=�6�   $	��672��6�   $��	10,10,0,0q�6�   �� 5/   
   -   5         ��Name$ LBL_TITRE_etatAcces��5/       K� ZIndex$ 1��6-   $��ETAT DE L'ACCES :��6?   $
��Left=�6[    J� RenderTransformOrigin$ 0.308,0.713&�6v   $	��525��6�   $��1��6�   $��20(�6�   $��
91,161,0,0q�6�   $��2��6�   $��36��6�   $	��Top=�6�   �� 50   
   -   5        $ LBL_etatAcces��50      $ 2��6'   $��Center=�69   $��Center=�6]   $��FERMER��6   $��91,38,104,0q�6�   $	��108��6�   $	��Top=�6�   $��2��6�   $��48(�6�   ���	#FF91FF96$
��Bold"�6�   �� 51   
   -   5         ��Name$ BR_etatAcces��51      ���	#FF008919$ 1��6?   $��1q�6Q   $��91,17,104,0q�6e   ���	#FF28E24A$	��150��6�   $	��Top=�6�   $��2��6�    J� Effect 52      3  -System.Windows.Media.Effects.DropShadowEffect  53         
BlurRadius$ 6Q�6#      	Direction$ 0Q�62      RenderingBias$ Quality=�6@      ShadowDepth$ 0Q�6X   55      �� 59   
   -   5        $ LBL_TITRE_etatCycle��59      $ 1��6-   $��ETAT DU CYCLE :��6?   $
��Left=�6Y   $	��525��6t   $��3��6�   $��20(�6�   $��36��6�   $	��Top=�6�   $��91,2,0,0q�6�   �� 5:   
   -   5        $ LBL_etatCycle��5:      $ 2��6'   $��Center=�69   $��Center=�6]   $��AUCUN CYCLE��6   $��91,65,104,0q�6�   ���	#3FFFFFFF$��48(�6�   $
��Bold"�6�   $��3��6�   $	��108��6  $	��Top=�6  ��6�   Z� 6  �� 5;   
   -   5        $ BR_etatCycle��5;      ���	#FF208525$ 1��6?   $��1q�6Q   $��91,43,104,0q�6e   ���	#FF23BD2A$��3��6�   $	��150��6�   $	��Top=�6�    5<        5=      $ 5Q�6#   $ 0Q�62   $ Quality=�6@   $ 0Q�6X   5?      �� 5C   
   -   5         ��Name$ bt_Gear��5C      $��10,296,1308,10q�6"   $��3��6;   #��[" �6H   .��5D      ]� .+�5E      �� $��img/gear.png��6   $ 
0.69,1.071&�6.   5F      5G      �� 5K   
   -	   5        $	
 Gif��5K         AnimatedSource$ img/ripple.gif��6   $��1275,261,10,10q�6P   $��3��6h   �� 5O   
   -
   5        $
 RFID_status��5O      $��img/RFID_OFF.png��6%   $��36��6?   $��1319,9,10,0q�6K   $��2��6`   $	��Top=�6q   �� 5P   
   -   5        $
 
BDD_status��5P      $��img/BDD_OFF.png��6$   $��36��6=   $��1277,10,52,0q�6I   $��2��6_   $	��Top=�6p   �� 5Q   
   -   5        $
 AUTOMATE_status��5Q      $��img/AUTOMATE_OFF.png��6)   $��30��6G   $��1241,12,94,0q�6S   $��2��6i   $	��Top=�6z   5S      5T      