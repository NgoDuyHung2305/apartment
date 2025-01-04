import { DarkTheme, DefaultTheme, ThemeProvider } from '@react-navigation/native';
import { useFonts } from 'expo-font';
import { Slot, Stack, useRouter } from 'expo-router';
import * as SplashScreen from 'expo-splash-screen';
import { StatusBar } from 'expo-status-bar';
import { useEffect, useState } from 'react';
import 'react-native-reanimated';
import { View, ActivityIndicator } from 'react-native';

import { useColorScheme } from '@/hooks/useColorScheme';

// Prevent the splash screen from auto-hiding before asset loading is complete.
SplashScreen.preventAutoHideAsync();

export default function RootLayout() {
  const colorScheme = useColorScheme();
  const [loaded] = useFonts({
    SpaceMono: require('../assets/fonts/SpaceMono-Regular.ttf'),
  });

  const router = useRouter();
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const checkLogin = async () => {
      // Giả lập kiểm tra đăng nhập
      const userToken = false;  // Thay bằng logic kiểm tra token thực tế
      if (!userToken) {
        setIsLoggedIn(false);  // Nếu chưa đăng nhập
      } else {
        setIsLoggedIn(true);
      }
      setLoading(false);
    };
    checkLogin();
  }, []);

  useEffect(() => {
    if (!loading && !isLoggedIn) {
      // Điều hướng sau khi Slot đã được render
      router.replace('/login');
    }
  }, [loading, isLoggedIn]);

  if (loading) {
    // Hiển thị màn hình loading trong khi kiểm tra trạng thái
    return (
      <View style={{ flex: 1, justifyContent: 'center', alignItems: 'center' }}>
        <ActivityIndicator size="large" color="#0000ff" />
      </View>
    );
  }

  return <Slot />;
}
