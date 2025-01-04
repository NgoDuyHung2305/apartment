import { ThemedText } from '@/components/ThemedText';
import { ThemedView } from '@/components/ThemedView';

export default function ReceiptScreen() {
  return (
    <ThemedView>
      <ThemedText type="title">Receipt</ThemedText>
      <ThemedText>Welcome to your receipts tab!</ThemedText>
    </ThemedView>
  );
}
