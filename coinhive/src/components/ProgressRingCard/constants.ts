import type { ProgressRingCard } from './types';
import { Moon } from 'lucide-react';

export const DEFAULT: ProgressRingCard = {
    icon: Moon,
    title: "Health",
    label: "hr",
    subtitle: "Sleeping of 8 hours is important for you health",
    maxValue: 24,
    value: 8,
    radius: 50,
}