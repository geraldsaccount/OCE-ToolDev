   M S B A M L   `   `   `  ????B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:WpfApp1.TilePalette.ViewsWpfApp1.TilePalette.Views  5      O,clr-namespace:WpfApp1.TilePalette.ViewModelsWpfApp1.TilePalette.ViewModels  5      /    )WpfApp1.TilePalette.Views.TileUserControl   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:WpfApp1.TilePalette.Views     5      ;vm,clr-namespace:WpfApp1.TilePalette.ViewModels     5        ? DataContext  5
      )   #WpfApp1.TilePalette.ViewModels.Tile  5   
   .??5      ?  ? ColumnDefinitions 5   
   ?? 5      $	??100 ?6   ?? 5      $??1* ?6    ? RowDefinitions 5   
   ?? 5      $??30 ?6   ?? 5      $??30 ?6   ?? 5      $??30 ?6   ?? 5      $??30 ?6   ?? 5      $??30 ?6   ?? 5      $??1* ?6   .+?5   
   ?? $??
Tile Name:??6   $
??Left=?6%   $??Center=?6@   ?? 5   
   $??1??6   $??0,5,5,0q?6"   ??63   ?? *Name+?? 5   
   $??Is Walkable:??6   $
??Left=?6'   $??Center=?6B   $??1??6]   ?? 5   
   $??1??6   $??1??6    $??Center=?60    d? 	IsChecked 6K   ?? *
IsWalkable+?? 5!   
   $??
Move Cost:??6   $
??Left=?6%   $??Center=?6@   $??2??6[   ?? 5"   
   $??2??6   $??1??6   $??0,5,5,0q?6/   ??6@   ?? *MovementCost+?? 5$   
   $??Defense Bonus:??6   $
??Left=?6)   $??Center=?6D   $??3??6_   ?? 5%   
   $??3??6   $??1??6   $??0,5,5,0q?6/   ??6@   ?? *DefenceBonus+5&      5'      