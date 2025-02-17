import MarkdownIt from "markdown-it";
import DOMPurify from "dompurify";

const md = new MarkdownIt();

export function transformPlainTextToMarkdown(text) {
  // Bold the header "Key services include:" by replacing it with markdown-syntax.
  text = text.replace(/Key services include:/g, "**Key services include:**");

  const divider = "**Key services include:**";
  const parts = text.split(divider);

  if (parts.length < 2) {
    // If divider not found, add newline breaks after sentences.
    return text.replace(/\.\s+/g, ".\n");
  }

  let intro = parts[0].trim();
  let listText = parts[1].trim();
  // Insert a newline after each period to separate sentences.
  listText = listText.replace(/\.\s+/g, ".\n");

  // Split the list text into sentences and then add a dash at the beginning of each sentence.
  const sentences = listText
    .split("\n")
    .filter((sentence) => sentence.trim().length > 0);
  listText = sentences.map((sentence) => `- ${sentence.trim()}`).join("\n");

  return `${intro}\n\n${divider}\n\n${listText}`;
}

export function renderDescription(description) {
  // First, transform the plain text into Markdown formatting.
  const markdownText = transformPlainTextToMarkdown(description);
  // Then convert it to HTML with markdown-it.
  const rawHtml = md.render(markdownText);
  // Finally, sanitize the HTML before returning.
  return DOMPurify.sanitize(rawHtml);
}
