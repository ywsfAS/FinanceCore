import type { SparklineCard } from './types'
import { Heart } from 'lucide-react';

export const DEFAULT: SparklineCard = {
    id: Date.now().toString(),
    icon: Heart,
    title: "Heart",
    subtitle: "Your Heart rate normal, heart 80 bpm reading"
}