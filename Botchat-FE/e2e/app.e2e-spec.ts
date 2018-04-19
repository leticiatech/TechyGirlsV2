import { FrontSorteoPage } from './app.po';

describe('front-sorteo App', function() {
  let page: FrontSorteoPage;

  beforeEach(() => {
    page = new FrontSorteoPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
