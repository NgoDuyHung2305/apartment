import { ThemedText } from '@/components/ThemedText';
import { ThemedView } from '@/components/ThemedView';

export default function OrderScreen() {
  return (
    <ThemedView>
      <ThemedText type="title">Order</ThemedText>
      <ThemedText>Welcome to your orders tab!</ThemedText>
    </ThemedView>
  );
}
