namespace WebsiteGenerator.Templates;

public static class CssTemplate
{
    public static string Generate()
    {
        return """
        /* Reset */
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body {
            font-family: Arial, sans-serif;
            margin: 40px;
            line-height: 1.6;
        }

        nav ul {
            list-style: none;
            display: flex;
            gap: 15px;
            margin-bottom: 20px;
        }

        nav a {
            text-decoration: none;
            color: #007acc;
        }

        nav a:hover {
            text-decoration: underline;
        }
        """;
    }
}
