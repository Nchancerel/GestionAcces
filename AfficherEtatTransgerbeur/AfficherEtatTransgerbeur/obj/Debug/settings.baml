   M S B A M L   `   `   `  ����S  OAfficherEtatTransgerbeur, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:AfficherEtatTransgerbeurAfficherEtatTransgerbeur  5   	   T PMahApps.Metro, Version=1.4.1.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5	   	   '    !AfficherEtatTransgerbeur.settings   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   J FWpfAnimatedGif, Version=1.4.14.0, Culture=neutral, PublicKeyToken=null,gif"http://wpfanimatedgif.codeplex.com  5   	   Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5   	   2local&clr-namespace:AfficherEtatTransgerbeur  5   	   JControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro  5	   	   "  WpfAnimatedGif.ImageBehavior    AnimateInDesignMode  .  � Title$ settings��5   	   $	��512��6   $��731.12��6'    � 
ResizeMode$ NoResize=�66    � WindowStyle$
 None=�6L   $��center=�6_    � WindowStartupLocation$ CenterScreen=�6}   (  "MahApps.Metro.Controls.MetroWindow   ShowTitleBar .  .��5      � ��5   
   
� 5      .��5      �  � Color$ White��6    � Offset$ 0Q�6-   � 5      $ 	#FFE8E8E8��6   $ 1Q�61   5      .+�5   
   �� ���	#FF979797$��1q�6)   $
��Left=�6=   $��360,40,0,40q�6X   $��1��6m    J� RenderTransformOrigin$ 0.5,0.5&�6w   z�5      V� 5      .~�5      �� 	 �� ScaleX$	 -1Q�6%   �� 5      �� 5      T� 5      5      5      �� 5   
   -   5        
 ��Name$
 LBL_BDD��5      $��Base de donnée��6!   $
��Left=�6:   $��	10,30,0,0q�6U   $	��Top=�6h   $��48��6�   $	��341��6�   $��36(�6�   $��	Tw Cen MT)�6�   ���	#FF979797���	#FF5757576�   �� 5    
   -   5        $
 LBL_BDD_HostAddress��5       $��Adresse de l'hote��6-   $
��Left=�6I   $��
10,100,0,0q�6d   $	��Top=�6x   $��32��6�   $	��341��6�   $��24(�6�   $��	Tw Cen MT)�6�   ���	#FF979797���	#FF5757576�   �� 5!   
   -   5         ��Name$ BDD_adresse��5!      $
��Left=�6'   $��34��6B   $��
10,140,0,0q�6N    �� TextWrapping$
 Wrap=�6b   $�� ��6v   $
��Left=�6~   $��Center=�6�   $	��Top=�6�   $	��341��6�   ���	#FF9797976�   �� 5"   
   -   5        $
 LBL_BDD_DatabaseName��5"      $��Nom de la base de donnée��6.   $
��Left=�6Q   $��
10,190,0,0q�6l   $	��Top=�6�   $��32��6�   $	��341��6�   $��24(�6�   $��	Tw Cen MT)�6�   ���	#FF979797���	#FF5757576�   �� 5#   
   -   5        $ BDD_nomBase��5#      $
��Left=�6'   $��34��6B   $��
10,230,0,0q�6N   $
 Wrap=�6b   $�� ��6v   $
��Left=�6~   $��Center=�6�   $	��Top=�6�   $	��341��6�   ���	#FF9797976�   �� 5$   
   -   5        $
 LBL_BDD_Username��5$      $��Nom d'utilisateur��6*   $
��Left=�6F   $��
10,280,0,0q�6a   $	��Top=�6u   $��32��6�   $	��341��6�   $��24(�6�   $��	Tw Cen MT)�6�   ���	#FF979797���	#FF5757576�   �� 5%   
   -   5        $ BDD_nomUtilisateur��5%      $
��Left=�6.   $��34��6I   $��
10,320,0,0q�6U   $
 Wrap=�6i   $�� ��6}   $
��Left=�6�   $��Center=�6�   $	��Top=�6�   $	��341��6�   ���	#FF9797976�   �� 5&   
   -   5        $
 LBL_BDD_password��5&      $��Mot de passe��6*   $
��Left=�6A   $��
10,370,0,0q�6\   $	��Top=�6p   $��32��6�   $	��341��6�   $��24(�6�   $��	Tw Cen MT)�6�   ���	#FF979797���	#FF5757576�   G� 5'   
   -	   5         G�Name$ BDD_motDePasse��5'      $
��Left=�6.   $��34��6I   $��
10,410,0,0q�6U   $
��Left=�6i   $��Center=�6�   $	��Top=�6�   $	��341��6�   ���	#FF979797 G� PasswordChar$ *��6�   �� 5*   
   -
   5        $
 LBL_ADDRESS��5*      $��
Adresse IP��6%   $
��Left=�6:   $��
377,30,0,0q�6U   $	��Top=�6i   $��48��6�   $	��341��6�   $��36(�6�   $��	Tw Cen MT)�6�   ���	#FF979797���	#FF5757576�   �� 5+   
   -   5        $
 LB_IP_automate��5+      $��Adresse IP de l'automate��6(   $
��Left=�6K   $��377,100,0,0q�6f   $	��Top=�6{   $��32��6�   $	��341��6�   $��24(�6�   $��	Tw Cen MT)�6�   ���	#FF979797���	#FF5757576�   �� 5,   
   -   5        $ IP_automate��5,      $
��Left=�6'   $��34��6B   $��377,140,0,0q�6N   $
 Wrap=�6c   $�� ��6w   $
��Left=�6   $��Center=�6�   $	��Top=�6�   $	��341��6�   ���	#FF9797976�   �� 5-   
   -   5        $
 LB_IP_stationRFID��5-      $#��Adresse IP de la station RFID��6+   $
��Left=�6S   $��377,190,0,0q�6n   $	��Top=�6�   $��32��6�   $	��341��6�   $��24(�6�   $��	Tw Cen MT)�6�   ���	#FF979797���	#FF5757576�   �� 5.   
   -   5        $ IP_stationRFID��5.      $
��Left=�6*   $��34��6E   $��377,230,0,0q�6Q   $
 Wrap=�6f   $�� ��6z   $
��Left=�6�   $��Center=�6�   $	��Top=�6�   $	��341��6�   ���	#FF9797976�   �� 51   
   -   5         ��Name$ TestConnection_BDD��51      $-��'Test de connection a la base de donnée��6-   $
��Left=�6^   $��380,288,0,0q�6y   $	��Top=�6�   $	��338��6�   $��33��6�   �� 52   
   -   5        $ TestConnection_AUTOMATE��52      $+��%test de communication avec l'automate��62   $
��Left=�6b   $��380,350,0,0q�6}   $	��Top=�6�   $	��338��6�   $��32��6�   �� 53   
   -   5        $ TestConnection_RFID��53      $0��*test de communication avec la station RFID��6.   $
��Left=�6c   $��380,411,0,0q�6~   $	��Top=�6�   $	��338��6�   $��33��6�   �� 56   
   -   5        $ 	BT_FERMER��56      $��FERMER��6$   $
��Left=�65   $��223,475,0,0q�6P   $	��Top=�6e   $	��120��6}   6�   �� 57   
   -   5        $ 
BT_VALIDER��57      $��VALIDER��6%   $
��Left=�67   $��380,475,0,0q�6R   $	��Top=�6g   $	��120��6   6�   59      5:      