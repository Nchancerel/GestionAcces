   M S B A M L   `   `   `  ����S  OAfficherEtatTransgerbeur, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:AfficherEtatTransgerbeurAfficherEtatTransgerbeur  5   	   T PMahApps.Metro, Version=1.4.1.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5	   	   (    "AfficherEtatTransgerbeur.PopUpRFID   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   J FWpfAnimatedGif, Version=1.4.14.0, Culture=neutral, PublicKeyToken=null,gif"http://wpfanimatedgif.codeplex.com  5   	   Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5   	   2local&clr-namespace:AfficherEtatTransgerbeur  5   	   JControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro  5	   	   -   5        "  WpfAnimatedGif.ImageBehavior    AnimateInDesignMode  .  � Title$ 	PopUpRFID��5   	   $	��256��6   $
��auto��6(    � 
ResizeMode$ NoResize=�65    � WindowStyle$
 None=�6K   $��Stretch=�6^    � WindowStartupLocation$ CenterScreen=�6}   (  "MahApps.Metro.Controls.MetroWindow   ShowTitleBar .  .��5      � ��5   
   
� 5      .��5      �  � Color$ White��6    � Offset$ 0Q�6-   � 5      $ 	#FFE8E8E8��6   $ 1Q�61   5      .+�5   
   �� -   5         ��Name$ AUTORISE��5      $��img/autorise.png��6"   $
��Left=�6<   $��54��6W   $��350,102,0,0q�6c   $	��Top=�6x   $��68��6�   $y�Hidden=�6�   �� 5   
   -   5        $ REFUSE��5      $��img/refuse.png��6    $
��Left=�68   $��36��6S   $��350,110,0,0q�6_   $	��Top=�6t   $��37��6�   $y�Hidden=�6�   �� 5   
   -   5        $ loader��5      	   AnimatedSource$	 img/ring.gif��6!   $��	0,80,0,80q�6Q   �� 5   
   -   5        
 ��Name$
 
label_info��5      $�� ��6$   $��Center=�6/   $��Center=�6S   $��10,104,10,105q�6u   $��24(�6�   $��	Tw Cen MT)�6�   �� 5   
   -   5        $
 label_accesRefuse��5      $�� ��6+   $��Center=�66   $��Center=�6Z   $��10,104,10,105q�6|   $��24(�6�   $��	Tw Cen MT)�6�   �� 5   
   -   5        $
 label_tagRFID��5      $�� ��6'   $��Center=�62   $��Center=�6V   $
��Left=�6x   $��
10,199,0,0q�6�   $	��Top=�6�   $	��748��6�   $��47��6�   $��24(�6�   $��	Tw Cen MT)�6�   5      5      