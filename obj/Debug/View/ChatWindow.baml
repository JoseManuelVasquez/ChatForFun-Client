   M S B A M L   `   `   `  ����L  HChatForFun-Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:ChatForFun_Client.ViewChatForFun_Client.View  5   	   '    !ChatForFun_Client.View.ChatWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:ChatForFun_Client.View     5   	     � Title$
  Chat��5   	   $	��450��6   $	��800��6#   $	��500��6/   $	��450��6>   ���	#FF2F2F37��6e   Z� .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��2* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��* �6   .+�5   
   � -   5         �Name$ greetingGrid��5      $��0��6%   $��0��62   ���	#FF1A1A1A$��0q�6Y   $��50��6d   .+�5      �� -   5         ��Name$ 
tbGreeting��5      $��Center=�6,   $
��Wrap=�6I   $��Center=�6]   ���	#FFCCCCCC$��Hi Jose!��6�   $��36(�6�   5      � 5   
   -   5        $ messageGrid��5      $��1��6$   $��0��61   ���	#FF011D28$��0,1,0,0q�6X   .+�5      �� -   5         ��Name$ messageTabControl��5      ���	#FF011D28��6K   Z� .@�5      �� $��Welcome!��6   .��5      � ���	#FF011D28.+�5      �� $��0q�6$   $
��Wrap=�6/   $!��Click a friend for chatting��6C   
�������$��22(�6y   $��Center=�6�   $��Center=�6�   5      5      5      5       � 5"   
   -   5        $ friendsGrid��5"      $��1��6$   $��0��64   ���	#FF1A1A1A$��1,0,0,0q�6X   $��2��6i    5#      �� 5$      $��7* �6#   �� 5%      $
��125* �6#    5'      �� 5(      $��50 �6    �� 5)      $��* �6    �� 5*      $��50 �6    .+�5-      �� -   5         ��Name$ imgAddFriend��5-      $��0��6+   $��30��68   $��30��6C    � ToolTip$ Add a Friend��6O   $��Right=�6f   $��
0,10,10,10q�6�    � Cursor$
 Hand��6�   $��1��6�   ��5.      �� 5/      .R�50      �� $;��5/ChatForFun-Client;Component/resources/add_friend.png��6    51      53      � 55      $��1��6   $��2��6     56      �� 57      $��30 �6$   �� 58      $��* �6$   .+�5;      � $��0��6   ���	#FF0C394B.+�5<      �� $��Friends��6    ���	#FFCCCCCC$��Center=�6F   $��20(�6a   $��Center=�6o   5=      �� 5?      -   5        	 ��Name$	 svFriend��5?      $��1��61   $
��Auto=�6T   ��6>   Z� .��5@      �� -   5        
 ��Name$
 spFriend��5@   !   $��15,10q�63   5A      5B      �� 5D      -	   5        $ 	btnLogout��5D      $��2��6(   $��Logout��65   ���	#FFCCCCCC���	#FFCCCCCC���	#FF011D28$��67.542,10,82.001,10q�6�   $
 Hand��6�   $ Logout��6�   $��1��6�   6�   5E      5F      5G      