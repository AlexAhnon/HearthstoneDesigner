   M S B A M L   `   `   `  ����N  JHearthstoneDesigner, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HearthstoneDesignerHearthstoneDesigner  5   	   $    HearthstoneDesigner.TypeWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:HearthstoneDesigner     5   	     � Title$  Create a new type��5   	   $	��200��6#   $	��300��60    � 
ResizeMode$ NoResize=�6<    � WindowStartupLocation$ CenterScreen=�6R   .��5	      � .+�5
   
   �� $��
Type name:��6   $
��Left=�6%   $��	10,10,0,0q�6@   $	��Top=�6S   $��18(�6k   $
��Bold"�6y   $	��150��6�   $��35��6�   $��2��6�   �� 5   
   $��Minimum stats:��6   $
��Left=�6)   $��	10,45,0,0q�6D   $	��Top=�6W   $��18(�6o   $
��Bold"�6}   $	��150��6�   $��35��6�   $��2��6�   �� 5   
   $��Maximum stats:��6   $
��Left=�6)   $��	10,80,0,0q�6D   $	��Top=�6W   $��18(�6o   $
��Bold"�6}   $	��150��6�   $��35��6�   $��2��6�   �� 5   
   -   5         ��Name$ typeName��5      $
��Left=�6u   $��26��6�   $��
160,15,0,0q�6�    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��120��6�   $��16(�6�   $��Center=�6�   ��6$   �� *CardType.Name+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5   
   -   5        $ typeMinStats��5      $
��Left=�6}   $��26��6�   $��
160,50,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $��30��6�   $��16(�6�   $��Center=�6�   ��6(   �� *CardType.MinStats+$ TwoWay=�$ PropertyChanged=�6�   �� 5   
   -   5        $ maxMinStats��5      $
��Left=�6|   $��26��6�   $��
160,85,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $��30��6�   $��16(�6�   $��Center=�6�   ��6'   �� *CardType.MaxStats+$ TwoWay=�$ PropertyChanged=�6�   �� 5   
   $	��Add��6   $
��Left=�6H   $��
80,120,0,0q�6c   $	��Top=�6w   $	��120��6�   $��30��6�   ��6   �� *CreateNewTypeCommand+6�   5      5      