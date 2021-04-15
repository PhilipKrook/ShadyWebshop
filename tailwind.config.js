const defaultTheme = require('tailwindcss/defaultTheme')

module.exports = {
	purge: {
		enabled: process.env.NODE_ENV === 'production',
		content: ['./**/*.cshtml', './**/*.aspx', './**/*.cs']
	},
	darkMode: false,
	theme: {
		extend: {
			backgroundImage: theme => ({
				'hero-pattern': "url('/img/hero-pattern.svg')",
				'footer-texture': "url('/img/footer-texture.png')",
			   }),
			fontFamily: {
				sans: ['Inter var', 'Arial', 'Helvetica', 'sans-serif'],
				mono: ['JetBrains Mono', ...defaultTheme.fontFamily.mono],
			},
			typography: {
				DEFAULT: {
					css: {
						'pre code::before': {
							content: 'none',
						},
						'pre code::after': {
							content: 'none',
						},
					},
				},
			}
		},
	},
	variants: {
		extend: {
			outline: ['hover', 'active'],
			ringColor: ['hover', 'active'],
			ringOffsetColor: ['hover', 'active'],
			ringOffsetWidth: ['hover', 'active'],
			ringOpacity: ['hover', 'active'],
			ringWidth: ['hover', 'active'],
		},
	},
	plugins: [
		require('@tailwindcss/typography')
	],
}
